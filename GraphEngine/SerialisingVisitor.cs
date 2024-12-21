// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

public class SerialisingVisitor(NodeWithGraph node) : Linq.ExpressionVisitor()
{
    private readonly Dictionary<object, NodeWithGraph> mapping = [];
    private readonly NodeWithGraph node = node ?? throw new ArgumentNullException(nameof(node));
    private readonly Stack<NodeWithGraph> path = new();
    private bool initialised;

    private NodeWithGraph Current => path.Peek();

    private NodeWithGraph this[object index]
    {
        get
        {
            if (!initialised)
            {
                mapping[index] = node;
                initialised = true;

                return node;
            }

            if (!mapping.TryGetValue(index, out var current))
            {
                current = mapping[index] = node.Graph.CreateBlankNode().In(node.Graph);
            }

            return current;
        }
    }

    public override Linq.Expression Visit(Linq.Expression? node)
    {
        using (Wrap(node))
        {
            return base.Visit(node);
        }
    }

    protected override Linq.Expression VisitBinary(Linq.BinaryExpression node)
    {
        if (node.NodeType == Linq.ExpressionType.ArrayIndex)
        {
            _ = new ArrayIndex(Current)
            {
                Array = VisitCacheParse(node.Left),
                Index = VisitCacheParse(node.Right),
            };
        }
        else
        {
            Binary binary;

            if (node.IsReferenceComparison())
            {
                if (node.NodeType == Linq.ExpressionType.Equal)
                {
                    binary = new ReferenceEqual(Current);
                }
                else
                {
                    binary = new ReferenceNotEqual(Current);
                }
            }
            else
            {
                binary = Binary.Create(Current, node.NodeType);

                if (node.Method is MethodInfo method)
                {
                    binary.Method = VisitMethod(method);
                }

                if (node.Conversion is Linq.LambdaExpression lambda)
                {
                    binary.Conversion = new Lambda(VisitCache(lambda));
                }

                if (node.IsLiftedToNull)
                {
                    binary.LiftToNull = true;
                }
            }

            binary.Left = VisitCacheParse(node.Left);
            binary.Right = VisitCacheParse(node.Right);
        }

        return node;
    }

    protected override Linq.Expression VisitBlock(Linq.BlockExpression node)
    {
        var block = new Block(Current);

        if (!Extensions.AreEquivalent(node.Type, node.Expressions.Last().Type))
        {
            block.Type = VisitType(node.Type);
        }

        foreach (var variable in node.Variables)
        {
            block.Variables.Add(new Parameter(VisitCache(variable)));
        }

        foreach (var blockExpression in node.Expressions)
        {
            block.Expressions.Add(VisitCacheParse(blockExpression));
        }

        return node;
    }

    protected override Linq.Expression VisitConditional(Linq.ConditionalExpression node)
    {
        Condition condition;

        if (node.Type == typeof(void))
        {
            if (node.IfFalse is Linq.DefaultExpression defaultExpression && defaultExpression.Type == typeof(void))
            {
                condition = new IfThen(Current);
            }
            else
            {
                condition = new IfThenElse(Current)
                {
                    IfFalse = VisitCacheParse(node.IfFalse),
                };
            }
        }
        else
        {
            condition = new Condition(Current)
            {
                IfFalse = VisitCacheParse(node.IfFalse),
            };

            if (node.Type != node.IfTrue.Type)
            {
                condition.Type = VisitType(node.Type);
            }
        }

        condition.Test = VisitCacheParse(node.Test);
        condition.IfTrue = VisitCacheParse(node.IfTrue);

        return node;
    }

    protected override Linq.Expression VisitConstant(Linq.ConstantExpression node)
    {
        var constant = new Constant(Current)
        {
            Value = node.Value,
        };

        if (node.Type != node.Value.GetType())
        {
            constant.Type = VisitType(node.Type);
        }

        return node;
    }

    protected override Linq.Expression VisitDebugInfo(Linq.DebugInfoExpression node)
    {
        var debugInfo = node.IsClear switch
        {
            true => new ClearDebugInfo(Current),
            false => new DebugInfo(Current),
        };

        debugInfo.Document = VisitSymbolDocument(node.Document);
        debugInfo.StartLine = node.StartLine;
        debugInfo.StartColumn = node.StartLine;
        debugInfo.EndLine = node.EndLine;
        debugInfo.EndColumn = node.EndLine;

        return node;
    }

    protected override Linq.Expression VisitDefault(Linq.DefaultExpression node)
    {
        if (node.Type == typeof(void))
        {
            _ = new Empty(Current);
        }
        else
        {
            _ = new Default(Current)
            {
                Type = VisitType(node.Type),
            };
        }

        return node;
    }

    protected override Linq.Expression VisitDynamic(Linq.DynamicExpression node)
    {
        var dynamicNode = new Dynamic(Current)
        {
            Binder = VisitBinder(node.Binder),
            ReturnType = VisitType(node.Type),
        };

        foreach (var argument in node.Arguments)
        {
            dynamicNode.Arguments.Add(VisitCacheParse(argument));
        }

        return node;
    }

    protected override Linq.ElementInit VisitElementInit(Linq.ElementInit node)
    {
        using (Wrap(node))
        {
            var elementInit = new ElementInit(Current)
            {
                AddMethod = VisitMethod(node.AddMethod),
            };

            foreach (var argument in node.Arguments)
            {
                elementInit.Arguments.Add(VisitCacheParse(argument));
            }

            return node;
        }
    }

    protected override Linq.Expression VisitExtension(Linq.Expression node)
    {
        if (node is Linq.DynamicExpression dynamicExpression)
        {
            return VisitDynamic(dynamicExpression);
        }

        throw new InvalidOperationException($"unknown extension {node}");
    }

    protected override Linq.Expression VisitGoto(Linq.GotoExpression node)
    {
        var @goto = BaseGoto.Create(Current, node.Kind);

        @goto.Target = new Target(this[VisitLabelTarget(node.Target)]);

        if (node.Value is not null)
        {
            @goto.Value = VisitCacheParse(node.Value);
        }

        if (node.Type != typeof(void))
        {
            @goto.Type = VisitType(node.Type);
        }

        return node;
    }

    protected override Linq.Expression VisitIndex(Linq.IndexExpression node)
    {
        if (node.Indexer is not null)
        {
            var property = new Property(Current)
            {
                Expression = VisitCacheParse(node.Object),
                Name = node.Indexer.Name,
            };

            foreach (var index in node.Arguments)
            {
                property.Arguments.Add(VisitCacheParse(index));
            }
        }
        else
        {
            var arrayAccess = new ArrayAccess(Current)
            {
                Array = VisitCacheParse(node.Object),
            };

            foreach (var index in node.Arguments)
            {
                arrayAccess.Indexes.Add(VisitCacheParse(index));
            }
        }

        return node;
    }

    protected override Linq.LabelTarget VisitLabelTarget(Linq.LabelTarget? node)
    {
        using (Wrap(node))
        {
            var target = new Target(Current);

            if (node.Type != typeof(void))
            {
                target.Type = VisitType(node.Type);
            }

            if (node.Name is not null)
            {
                target.Name = node.Name;
            }

            return node;
        }
    }

    protected override Linq.Expression VisitListInit(Linq.ListInitExpression node)
    {
        var listInit = new ListInit(Current)
        {
            NewExpression = new New(VisitCache(node.NewExpression)),
        };

        foreach (var initializer in node.Initializers)
        {
            listInit.Initializers.Add(new ElementInit(this[VisitElementInit(initializer)]));
        }

        return node;
    }

    protected override Linq.Expression VisitLambda<T>(Linq.Expression<T> node)
    {
        var lambda = new Lambda(Current)
        {
            Body = VisitCacheParse(node.Body),
        };

        foreach (var parameter in node.Parameters)
        {
            lambda.Parameters.Add(new Parameter(VisitCache(parameter)));
        }

        return node;
    }

    protected override Linq.Expression VisitLoop(Linq.LoopExpression node)
    {
        var loop = new Loop(Current)
        {
            Body = VisitCacheParse(node.Body),
        };

        if (node.ContinueLabel is not null)
        {
            loop.Continue = new Target(this[VisitLabelTarget(node.ContinueLabel)]);
        }

        if (node.BreakLabel is not null)
        {
            loop.Break = new Target(this[VisitLabelTarget(node.BreakLabel)]);
        }

        return node;
    }

    protected override Linq.Expression VisitMember(Linq.MemberExpression node)
    {
        var memberAccess = node.Member.MemberType switch
        {
            MemberTypes.Field => new Field(Current) as MemberAccess,
            MemberTypes.Property => new Property(Current) as MemberAccess,
            var mt => throw new GraphEngineException($"unknown member type {mt}")
        };

        memberAccess.Name = node.Member.Name;

        if (node.Expression is not null)
        {
            memberAccess.Expression = VisitCacheParse(node.Expression);
        }

        if (node.Expression is null || node.Expression.Type != node.Member.DeclaringType)
        {
            memberAccess.Type = VisitType(node.Member.DeclaringType);
        }

        return node;
    }

    protected override Linq.MemberAssignment VisitMemberAssignment(Linq.MemberAssignment node)
    {
        _ = new Bind(Current)
        {
            Member = VisitMember(node.Member),
            Expression = VisitCacheParse(node.Expression),
        };

        return node;
    }

    protected override Linq.MemberBinding VisitMemberBinding(Linq.MemberBinding node)
    {
        using (Wrap(node))
        {
            return node switch
            {
                Linq.MemberAssignment binding => VisitMemberAssignment(binding),
                Linq.MemberMemberBinding binding => VisitMemberMemberBinding(binding),
                Linq.MemberListBinding binding => VisitMemberListBinding(binding),
                var n => throw new GraphEngineException($"unknown member binding {n}")
            };
        }
    }

    protected override Linq.Expression VisitMemberInit(Linq.MemberInitExpression node)
    {
        var memberInit = new MemberInit(Current)
        {
            NewExpression = new New(VisitCache(node.NewExpression)),
        };

        foreach (var binding in node.Bindings)
        {
            memberInit.Bindings.Add(BaseBind.Create(this[VisitMemberBinding(binding)], binding.BindingType));
        }

        return node;
    }

    protected override Linq.MemberListBinding VisitMemberListBinding(Linq.MemberListBinding node)
    {
        var listBinding = new ListBind(Current)
        {
            Member = VisitMember(node.Member),
        };

        foreach (var item in node.Initializers)
        {
            listBinding.Initializers.Add(new ElementInit(this[VisitElementInit(item)]));
        }

        return node;
    }

    protected override Linq.MemberMemberBinding VisitMemberMemberBinding(Linq.MemberMemberBinding node)
    {
        var memberBinding = new MemberBind(Current)
        {
            Member = VisitMember(node.Member),
        };

        foreach (var binding in node.Bindings)
        {
            memberBinding.Bindings.Add(BaseBind.Create(this[VisitMemberBinding(binding)], binding.BindingType));
        }

        return node;
    }

    protected override Linq.Expression VisitMethodCall(Linq.MethodCallExpression node)
    {
        var call = new Call(Current)
        {
            Method = VisitMethod(node.Method),
        };

        if (node.Object is not null)
        {
            call.Instance = VisitCacheParse(node.Object);
        }

        foreach (var argument in node.Arguments)
        {
            call.Arguments.Add(VisitCacheParse(argument));
        }

        return node;
    }

    protected override Linq.Expression VisitNew(Linq.NewExpression node)
    {
        _ = new New(Current)
        {
            Type = VisitType(node.Type),
        };

        return node;
    }

    protected override Linq.Expression VisitNewArray(Linq.NewArrayExpression node)
    {
        NewArray newArray;
        if (node.NodeType == Linq.ExpressionType.NewArrayBounds)
        {
            newArray = new NewArrayBounds(Current);
        }
        else
        {
            newArray = new NewArrayInit(Current);
        }

        newArray.Type = VisitType(node.Type.GetElementType());

        foreach (var expression in node.Expressions)
        {
            newArray.Expressions.Add(VisitCacheParse(expression));
        }

        return node;
    }

    protected override Linq.Expression VisitParameter(Linq.ParameterExpression node)
    {
        var parameter = new Parameter(Current)
        {
            Type = VisitType(node.Type),
        };

        if (node.Name is not null)
        {
            parameter.Name = node.Name;
        }

        return node;
    }

    protected override Linq.Expression VisitRuntimeVariables(Linq.RuntimeVariablesExpression node)
    {
        var runtimeVariables = new RuntimeVariables(Current);

        foreach (var variable in node.Variables)
        {
            runtimeVariables.Variables.Add(new Parameter(VisitCache(variable)));
        }

        return node;
    }

    protected override Linq.Expression VisitTypeBinary(Linq.TypeBinaryExpression node)
    {
        _ = new TypeBinary(Current)
        {
            ExpressionType = VisitExpressionType(node.NodeType),
            Expression = VisitCacheParse(node.Expression),
            Type = VisitType(node.TypeOperand),
        };

        return node;
    }

    protected override Linq.Expression VisitUnary(Linq.UnaryExpression node)
    {
        if (node.NodeType == Linq.ExpressionType.Throw)
        {
            if (node.Operand is null && node.Type == typeof(void))
            {
                _ = new Rethrow(Current);
            }

            var @throw = new Throw(Current);

            if (node.Operand is Linq.Expression value)
            {
                @throw.Value = VisitCacheParse(value);
            }

            if (node.Type is System.Type type)
            {
                @throw.Type = VisitType(type);
            }
        }
        else
        {
            var unary = Unary.Create(Current, node.NodeType);

            unary.Type = VisitType(node.Type);

            unary.Operand = VisitCacheParse(node.Operand);

            if (node.Method is MethodInfo method)
            {
                unary.Method = VisitMethod(method);
            }
        }

        return node;
    }

    private Expression VisitCacheParse(Linq.Expression node) => Expression.Parse(VisitCache(node));

    private NodeWithGraph VisitCache(Linq.Expression node) => this[Visit(node)];

    private ArgumentInfo VisitArgumentInfo(string argument)
    {
        using (Wrap(argument))
        {
            return new ArgumentInfo(Current);
        }
    }

    private Binder VisitBinder(CallSiteBinder callSiteBinder)
    {
        using (Wrap(callSiteBinder))
        {
            switch (callSiteBinder)
            {
                case InvokeMemberBinder invokeMember:
                    var invokeMemberBinder = new InvokeMember(Current)
                    {
                        Name = invokeMember.Name,
                    };

                    // Object member is invoked on
                    invokeMemberBinder.Arguments.Add(new ArgumentInfo(this[VisitArgumentInfo(string.Empty)]));

                    foreach (var argument in invokeMember.CallInfo.ArgumentNames)
                    {
                        invokeMemberBinder.Arguments.Add(new ArgumentInfo(this[VisitArgumentInfo(argument)]));
                    }

                    return invokeMemberBinder;

                case BinaryOperationBinder binaryOperation:
                    var binaryOperationBinder = new BinaryOperation(Current)
                    {
                        ExpressionType = VisitExpressionType(binaryOperation.Operation),
                    };

                    // Left operand
                    binaryOperationBinder.Arguments.Add(new ArgumentInfo(this[VisitArgumentInfo(string.Empty)]));

                    // Right operand
                    binaryOperationBinder.Arguments.Add(new ArgumentInfo(this[VisitArgumentInfo(string.Empty)]));

                    return binaryOperationBinder;

                case var unknown:
                    throw new GraphEngineException($"Unkown binder {unknown}");
            }
        }
    }

    private ExpressionType VisitExpressionType(Linq.ExpressionType expressionType)
    {
        using (Wrap(expressionType))
        {
            return ExpressionType.Create(expressionType, node.Graph);
        }
    }

    private Member VisitMember(MemberInfo member)
    {
        using (Wrap(member))
        {
            return new Member(Current)
            {
                Type = VisitType(member.DeclaringType),
                Name = member.Name,
            };
        }
    }

    private Method VisitMethod(MethodInfo method)
    {
        using (Wrap(method))
        {
            var methodNode = new Method(Current)
            {
                Type = VisitType(method.DeclaringType),
                Name = method.Name,
            };

            foreach (var type in method.GetGenericArguments())
            {
                methodNode.TypeArguments.Add(VisitType(type));
            }

            return methodNode;
        }
    }

    private SymbolDocument VisitSymbolDocument(Linq.SymbolDocumentInfo document)
    {
        using (Wrap(document))
        {
            return new SymbolDocument(Current)
            {
                FileName = document.FileName,
                Language = NullIfEmpty(document.Language),
                LanguageVendor = NullIfEmpty(document.LanguageVendor),
                DocumentType = NullIfEmpty(document.DocumentType),
            };
        }

        static Guid? NullIfEmpty(Guid guid) => guid == Guid.Empty ? (Guid?)null : guid;
    }

    private Type VisitType(System.Type type)
    {
        using (Wrap(type))
        {
            var t = new Type(Current)
            {
                Name = $"{type}, {type.Assembly}",
            };

            foreach (var argument in type.GenericTypeArguments)
            {
                t.Arguments.Add(VisitType(argument));
            }

            return t;
        }
    }

    private Wrapper Wrap(object node) => new(this, this[node]);

    private readonly struct Wrapper : IDisposable
    {
        private readonly SerialisingVisitor visitor;

        internal Wrapper(SerialisingVisitor visitor, NodeWithGraph node)
        {
            this.visitor = visitor;
            this.visitor.path.Push(node);
        }

        void IDisposable.Dispose() => visitor.path.Pop();
    }
}
