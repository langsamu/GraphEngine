// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class SerialisingVisitor : Linq.ExpressionVisitor
    {
        private readonly IDictionary<object, INode> mapping = new Dictionary<object, INode>();
        private readonly INode node;
        private readonly Stack<INode> path = new Stack<INode>();
        private bool initialised;

        public SerialisingVisitor(INode node)
          : base()
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
        }

        private INode Current => this.path.Peek();

        private INode this[object index]
        {
            get
            {
                if (!this.initialised)
                {
                    this.mapping[index] = this.node;
                    this.initialised = true;

                    return this.node;
                }

                if (!this.mapping.TryGetValue(index, out var current))
                {
                    current = this.mapping[index] = this.node.Graph.CreateBlankNode();
                }

                return current;
            }
        }

        public override Linq.Expression Visit(Linq.Expression node)
        {
            using (this.Wrap(node))
            {
                return base.Visit(node);
            }
        }

        protected override Linq.Expression VisitBinary(Linq.BinaryExpression node)
        {
            if (node.NodeType == Linq.ExpressionType.ArrayIndex)
            {
                _ = new ArrayIndex(this.Current)
                {
                    Array = this.VisitCacheParse(node.Left),
                    Index = this.VisitCacheParse(node.Right),
                };
            }
            else
            {
                var binary = Binary.Create(this.Current, node.NodeType);

                binary.Left = this.VisitCacheParse(node.Left);
                binary.Right = this.VisitCacheParse(node.Right);
            }

            return node;
        }

        protected override Linq.Expression VisitBlock(Linq.BlockExpression node)
        {
            var block = new Block(this.Current);

            foreach (var variable in node.Variables)
            {
                block.Variables.Add(new Parameter(this.VisitCache(variable)));
            }

            foreach (var blockExpression in node.Expressions)
            {
                block.Expressions.Add(this.VisitCacheParse(blockExpression));
            }

            return node;
        }

        protected override Linq.Expression VisitMethodCall(Linq.MethodCallExpression node)
        {
            var call = new Call(this.Current)
            {
                Method = node.Method.Name,
            };

            if (node.Object is object)
            {
                call.Instance = this.VisitCacheParse(node.Object);
            }
            else
            {
                call.Type = this.VisitType(node.Method.DeclaringType);
            }

            foreach (var type in node.Method.GetGenericArguments())
            {
                call.TypeArguments.Add(this.VisitType(type));
            }

            foreach (var argument in node.Arguments)
            {
                call.Arguments.Add(this.VisitCacheParse(argument));
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
                    condition = new IfThen(this.Current);
                }
                else
                {
                    condition = new IfThenElse(this.Current)
                    {
                        IfFalse = this.VisitCacheParse(node.IfFalse),
                    };
                }
            }
            else
            {
                condition = new Condition(this.Current)
                {
                    IfFalse = this.VisitCacheParse(node.IfFalse),
                };

                if (node.Type != node.IfTrue.Type)
                {
                    condition.Type = this.VisitType(node.Type);
                }
            }

            condition.Test = this.VisitCacheParse(node.Test);
            condition.IfTrue = this.VisitCacheParse(node.IfTrue);

            return node;
        }

        protected override Linq.Expression VisitConstant(Linq.ConstantExpression node)
        {
            var constant = new Constant(this.Current)
            {
                Value = node.Value,
            };

            if (node.Type != node.Value.GetType())
            {
                constant.Type = this.VisitType(node.Type);
            }

            return node;
        }

        protected override Linq.Expression VisitDebugInfo(Linq.DebugInfoExpression node)
        {
            var debugInfo = new DebugInfo(this.Current)
            {
                Document = this.VisitSymbolDocument(node.Document),
                StartLine = node.StartLine,
                StartColumn = node.StartLine,
                EndLine = node.EndLine,
                EndColumn = node.EndLine,
            };

            return node;
        }

        protected override Linq.Expression VisitDefault(Linq.DefaultExpression node)
        {
            new Default(this.Current).Type = this.VisitType(node.Type);

            return node;
        }

        protected override Linq.ElementInit VisitElementInit(Linq.ElementInit node)
        {
            using (this.Wrap(node))
            {
                var elementInit = new ElementInit(this.Current)
                {
                    AddMethod = this.VisitMethod(node.AddMethod),
                };

                foreach (var argument in node.Arguments)
                {
                    elementInit.Arguments.Add(this.VisitCacheParse(argument));
                }

                return node;
            }
        }

        protected override Linq.Expression VisitGoto(Linq.GotoExpression node)
        {
            var @goto = BaseGoto.Create(this.Current, node.Kind);

            @goto.Target = new Target(this[this.VisitLabelTarget(node.Target)]);

            if (node.Value is object)
            {
                @goto.Value = this.VisitCacheParse(node.Value);
            }

            if (node.Type != typeof(void))
            {
                @goto.Type = this.VisitType(node.Type);
            }

            return node;
        }

        protected override Linq.Expression VisitIndex(Linq.IndexExpression node)
        {
            if (node.Indexer is object)
            {
                var property = new Property(this.Current)
                {
                    Expression = this.VisitCacheParse(node.Object),
                    Name = node.Indexer.Name,
                };

                foreach (var index in node.Arguments)
                {
                    property.Arguments.Add(this.VisitCacheParse(index));
                }
            }
            else
            {
                var arrayAccess = new ArrayAccess(this.Current)
                {
                    Array = this.VisitCacheParse(node.Object),
                };

                foreach (var index in node.Arguments)
                {
                    arrayAccess.Indexes.Add(this.VisitCacheParse(index));
                }
            }

            return node;
        }

        protected override Linq.LabelTarget VisitLabelTarget(Linq.LabelTarget node)
        {
            using (this.Wrap(node))
            {
                var target = new Target(this.Current);

                if (node.Type != typeof(void))
                {
                    target.Type = this.VisitType(node.Type);
                }

                if (node.Name is object)
                {
                    target.Name = node.Name;
                }

                return node;
            }
        }

        protected override Linq.Expression VisitLoop(Linq.LoopExpression node)
        {
            var loop = new Loop(this.Current)
            {
                Body = this.VisitCacheParse(node.Body),
            };

            if (node.ContinueLabel is object)
            {
                loop.Continue = new Target(this[this.VisitLabelTarget(node.ContinueLabel)]);
            }

            if (node.BreakLabel is object)
            {
                loop.Break = new Target(this[this.VisitLabelTarget(node.BreakLabel)]);
            }

            return node;
        }

        protected override Linq.Expression VisitMember(Linq.MemberExpression node)
        {
            var memberAccess = node.Member.MemberType switch
            {
                MemberTypes.Field => (MemberAccess)new Field(this.Current),
                MemberTypes.Property => (MemberAccess)new Property(this.Current),
            };

            memberAccess.Name = node.Member.Name;

            if (node.Expression is object)
            {
                memberAccess.Expression = this.VisitCacheParse(node.Expression);
            }

            if (node.Expression is null || node.Expression.Type != node.Member.DeclaringType)
            {
                memberAccess.Type = this.VisitType(node.Member.DeclaringType);
            }

            return node;
        }

        protected override Linq.MemberAssignment VisitMemberAssignment(Linq.MemberAssignment node)
        {
            _ = new Bind(this.Current)
            {
                Member = this.VisitMember(node.Member),
                Expression = this.VisitCacheParse(node.Expression),
            };

            return node;
        }

        protected override Linq.MemberBinding VisitMemberBinding(Linq.MemberBinding node)
        {
            using (this.Wrap(node))
            {
                return node switch
                {
                    Linq.MemberAssignment binding => this.VisitMemberAssignment(binding),
                    Linq.MemberMemberBinding binding => this.VisitMemberMemberBinding(binding),
                    Linq.MemberListBinding binding => this.VisitMemberListBinding(binding),
                };
            }
        }

        protected override Linq.Expression VisitMemberInit(Linq.MemberInitExpression node)
        {
            var memberInit = new MemberInit(this.Current)
            {
                NewExpression = new New(this.VisitCache(node.NewExpression)),
            };

            foreach (var binding in node.Bindings)
            {
                memberInit.Bindings.Add(BaseBind.Create(this[this.VisitMemberBinding(binding)], binding.BindingType));
            }

            return node;
        }

        protected override Linq.MemberListBinding VisitMemberListBinding(Linq.MemberListBinding node)
        {
            var listBinding = new ListBind(this.Current)
            {
                Member = this.VisitMember(node.Member),
            };

            foreach (var item in node.Initializers)
            {
                listBinding.Initializers.Add(new ElementInit(this[this.VisitElementInit(item)]));
            }

            return node;
        }

        protected override Linq.MemberMemberBinding VisitMemberMemberBinding(Linq.MemberMemberBinding node)
        {
            var memberBinding = new MemberBind(this.Current)
            {
                Member = this.VisitMember(node.Member),
            };

            foreach (var binding in node.Bindings)
            {
                memberBinding.Bindings.Add(BaseBind.Create(this[this.VisitMemberBinding(binding)], binding.BindingType));
            }

            return node;
        }

        protected override Linq.Expression VisitNew(Linq.NewExpression node)
        {
            var @new = new New(this.Current)
            {
                Type = this.VisitType(node.Type),
            };

            return node;
        }

        protected override Linq.Expression VisitParameter(Linq.ParameterExpression node)
        {
            var parameter = new Parameter(this.Current)
            {
                Type = this.VisitType(node.Type),
            };

            if (node.Name is object)
            {
                parameter.Name = node.Name;
            }

            return node;
        }

        protected override Linq.Expression VisitUnary(Linq.UnaryExpression node)
        {
            var unary = Unary.Create(this.Current, node.NodeType);

            unary.Type = this.VisitType(node.Type);

            unary.Operand = this.VisitCacheParse(node.Operand);

            return node;
        }

        private Expression VisitCacheParse(Linq.Expression node) => Expression.Parse(this.VisitCache(node));

        private INode VisitCache(Linq.Expression node) => this[this.Visit(node)];

        private Member VisitMember(MemberInfo member)
        {
            using (this.Wrap(member))
            {
                var m = new Member(this.Current)
                {
                    Type = this.VisitType(member.DeclaringType),
                    Name = member.Name,
                };

                return m;
            }
        }

        private Method VisitMethod(MethodInfo method)
        {
            using (this.Wrap(method))
            {
                var m = new Method(this.Current)
                {
                    Type = this.VisitType(method.DeclaringType),
                    Name = method.Name,
                };

                return m;
            }
        }

        private SymbolDocument VisitSymbolDocument(SymbolDocumentInfo document)
        {
            using (this.Wrap(document))
            {
                var d = new SymbolDocument(this.Current)
                {
                    FileName = document.FileName,
                    Language = NullIfEmpty(document.Language),
                    LanguageVendor = NullIfEmpty(document.LanguageVendor),
                    DocumentType = NullIfEmpty(document.DocumentType),
                };

                return d;
            }

            static Guid? NullIfEmpty(Guid guid) => guid == Guid.Empty ? (Guid?)null : guid;
        }

        private Type VisitType(System.Type type)
        {
            using (this.Wrap(type))
            {
                var t = new Type(this.Current)
                {
                    Name = $"{type.Namespace}.{type.Name}, {type.Assembly}",
                };

                foreach (var argument in type.GenericTypeArguments)
                {
                    t.Arguments.Add(this.VisitType(argument));
                }

                return t;
            }
        }

        private IDisposable Wrap(object node)
        {
            return new Wrapper(this, this[node]);
        }

        private struct Wrapper : IDisposable
        {
            private readonly SerialisingVisitor visitor;

            internal Wrapper(SerialisingVisitor visitor, INode node)
            {
                this.visitor = visitor;
                this.visitor.path.Push(node);
            }

            void IDisposable.Dispose() => this.visitor.path.Pop();
        }
    }
}
