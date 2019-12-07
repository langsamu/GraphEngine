// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;
    using VDS.RDF.Nodes;

    public class SerialisingVisitor : ExpressionVisitor
    {
        private readonly Stack<INode> path = new Stack<INode>();
        private readonly IDictionary<object, INode> d = new Dictionary<object, INode>();
        private readonly INode n;
        private bool initialised;

        public SerialisingVisitor(INode n)
            : base()
        {
            this.n = n ?? throw new ArgumentNullException(nameof(n));
        }

        protected INode Current => this.path.Peek();

        public override Expression Visit(Expression node)
        {
            if (node is object)
            {
                this.path.Push(this.Lookup(node));
                var result = base.Visit(node);
                this.path.Pop();

                return result;
            }

            return null;
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            this.AddType(Vocabulary.Block);

            if (node.Variables.Any())
            {
                var root = this.Current.Graph.AssertList(node.Variables.Select(this.Lookup));
                this.AddStatement(Vocabulary.BlockVariables, root);
            }

            if (node.Expressions.Any())
            {
                var root = this.Current.Graph.AssertList(node.Expressions.Select(this.Lookup));
                this.AddStatement(Vocabulary.BlockExpressions, root);
            }

            return base.VisitBlock(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var type = node.NodeType switch
            {
                ExpressionType.Add => Vocabulary.Add,
                ExpressionType.AddAssign => Vocabulary.AddAssign,
                ExpressionType.AddAssignChecked => Vocabulary.AddAssignChecked,
                ExpressionType.AddChecked => Vocabulary.AddChecked,
                ExpressionType.And => Vocabulary.And,
                ExpressionType.AndAlso => Vocabulary.AndAlso,
                ExpressionType.AndAssign => Vocabulary.AndAssign,
                ExpressionType.ArrayIndex => Vocabulary.ArrayIndex,
                ExpressionType.Assign => Vocabulary.Assign,
                ExpressionType.Coalesce => Vocabulary.Coalesce,
                ExpressionType.Divide => Vocabulary.Divide,
                ExpressionType.DivideAssign => Vocabulary.DivideAssign,
                ExpressionType.Equal => Vocabulary.Equal,
                ExpressionType.ExclusiveOr => Vocabulary.ExclusiveOr,
                ExpressionType.ExclusiveOrAssign => Vocabulary.ExclusiveOrAssign,
                ExpressionType.GreaterThan => Vocabulary.GreaterThan,
                ExpressionType.GreaterThanOrEqual => Vocabulary.GreaterThanOrEqual,
                ExpressionType.LeftShift => Vocabulary.LeftShift,
                ExpressionType.LeftShiftAssign => Vocabulary.LeftShiftAssign,
                ExpressionType.LessThan => Vocabulary.LessThan,
                ExpressionType.LessThanOrEqual => Vocabulary.LessThanOrEqual,
                ExpressionType.Modulo => Vocabulary.Modulo,
                ExpressionType.ModuloAssign => Vocabulary.ModuloAssign,
                ExpressionType.Multiply => Vocabulary.Multiply,
                ExpressionType.MultiplyAssign => Vocabulary.MultiplyAssign,
                ExpressionType.MultiplyAssignChecked => Vocabulary.MultiplyAssignChecked,
                ExpressionType.MultiplyChecked => Vocabulary.MultiplyChecked,
                ExpressionType.NotEqual => Vocabulary.NotEqual,
                ExpressionType.Or => Vocabulary.Or,
                ExpressionType.OrAssign => Vocabulary.OrAssign,
                ExpressionType.OrElse => Vocabulary.OrElse,
                ExpressionType.Power => Vocabulary.Power,
                ExpressionType.PowerAssign => Vocabulary.PowerAssign,
                ExpressionType.RightShift => Vocabulary.RightShift,
                ExpressionType.RightShiftAssign => Vocabulary.RightShiftAssign,
                ExpressionType.Subtract => Vocabulary.Subtract,
                ExpressionType.SubtractAssign => Vocabulary.SubtractAssign,
                ExpressionType.SubtractAssignChecked => Vocabulary.SubtractAssignChecked,
                ExpressionType.SubtractChecked => Vocabulary.SubtractChecked,

                _ => throw new Exception()
            };

            this.AddType(type);
            this.AddStatement(Vocabulary.BinaryLeft, node.Left);
            this.AddStatement(Vocabulary.BinaryRight, node.Right);

            return base.VisitBinary(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            this.AddType(Vocabulary.Parameter);
            this.VisitType(node.Type, Vocabulary.ParameterType);

            if (node.Name is object)
            {
                this.AddStatement(Vocabulary.ParameterName, new StringNode(this.Current.Graph, node.Name));
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            this.AddType(Vocabulary.Loop);
            this.AddStatement(Vocabulary.LoopBody, node.Body);

            if (node.BreakLabel is object)
            {
                var breakNode = this.Lookup(node.BreakLabel);
                this.AddStatement(Vocabulary.GotoTarget, breakNode);
                this.path.Push(breakNode);
                this.VisitLabelTarget(node.BreakLabel);
                this.path.Pop();
            }

            if (node.ContinueLabel is object)
            {
                var continueNode = this.Lookup(node.ContinueLabel);
                this.AddStatement(Vocabulary.GotoTarget, continueNode);
                this.path.Push(continueNode);
                this.VisitLabelTarget(node.ContinueLabel);
                this.path.Pop();
            }

            this.Visit(node.Body);

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var type = node.NodeType switch
            {
                ExpressionType.ArrayLength => Vocabulary.ArrayLength,
                ExpressionType.Convert => Vocabulary.Convert,
                ExpressionType.ConvertChecked => Vocabulary.ConvertChecked,
                ExpressionType.Decrement => Vocabulary.Decrement,
                ExpressionType.Increment => Vocabulary.Increment,
                ExpressionType.IsFalse => Vocabulary.IsFalse,
                ExpressionType.IsTrue => Vocabulary.IsTrue,
                ExpressionType.Negate => Vocabulary.Negate,
                ExpressionType.NegateChecked => Vocabulary.NegateChecked,
                ExpressionType.Not => Vocabulary.Not,
                ExpressionType.OnesComplement => Vocabulary.OnesComplement,
                ExpressionType.PostDecrementAssign => Vocabulary.PostDecrementAssign,
                ExpressionType.PostIncrementAssign => Vocabulary.PostIncrementAssign,
                ExpressionType.PreDecrementAssign => Vocabulary.PreDecrementAssign,
                ExpressionType.PreIncrementAssign => Vocabulary.PreIncrementAssign,
                ExpressionType.Quote => Vocabulary.Quote,
                ExpressionType.Throw => Vocabulary.Throw,
                ExpressionType.TypeAs => Vocabulary.TypeAs,
                ExpressionType.UnaryPlus => Vocabulary.UnaryPlus,
                ExpressionType.Unbox => Vocabulary.Unbox,

                _ => throw new Exception()
            };

            this.AddType(type);
            this.AddStatement(Vocabulary.UnaryOperand, node.Operand);
            this.VisitType(node.Type, Vocabulary.UnaryType);

            return base.VisitUnary(node);
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            var type = node.Kind switch
            {
                GotoExpressionKind.Break => Vocabulary.Break,
                GotoExpressionKind.Continue => Vocabulary.Continue,
                GotoExpressionKind.Goto => Vocabulary.Goto,
                GotoExpressionKind.Return => Vocabulary.Return,

                _ => throw new Exception()
            };

            this.AddType(type);

            if (node.Value is object)
            {
                this.AddStatement(Vocabulary.GotoValue, node.Value);
            }

            this.VisitType(node.Type, Vocabulary.GotoType);

            var targetNode = this.Lookup(node.Target);
            this.AddStatement(Vocabulary.GotoTarget, targetNode);
            this.path.Push(targetNode);
            this.VisitLabelTarget(node.Target);
            this.path.Pop();

            this.Visit(node.Value);

            return node;
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            this.AddType(Vocabulary.Condition);
            this.AddStatement(Vocabulary.ConditionIfFalse, node.IfFalse);
            this.AddStatement(Vocabulary.ConditionIfTrue, node.IfTrue);
            this.AddStatement(Vocabulary.ConditionTest, node.Test);
            this.VisitType(node.Type, Vocabulary.ConditionType);

            return base.VisitConditional(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            this.AddType(Vocabulary.Constant);
            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.ConstantValue, this.Lookup(node.Value));
            this.VisitType(node.Type, Vocabulary.ConstantType);

            return base.VisitConstant(node);
        }

        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            this.VisitType(node.Type, Vocabulary.LabelType);

            return base.VisitLabelTarget(node);
        }

        private void VisitType(Type type, INode predicate)
        {
            if (type is object)
            {
                var typeNode = this.Lookup(type);
                this.AddStatement(predicate, typeNode);
                this.path.Push(typeNode);
                this.VisitType(type);
                this.path.Pop();
            }
        }

        private Type VisitType(Type type)
        {
            this.AddStatement(Vocabulary.TypeName, $"{type.Namespace}.{type.Name}");

            if (type.GenericTypeArguments.Any())
            {
                var nodes = new List<INode>();

                foreach (var typeArgument in type.GenericTypeArguments)
                {
                    var typeArgumentNode = this.Lookup(typeArgument);
                    nodes.Add(typeArgumentNode);
                    this.path.Push(typeArgumentNode);
                    this.VisitType(typeArgument);
                    this.path.Pop();
                }

                var root = this.Current.Graph.AssertList(nodes);
                this.AddStatement(Vocabulary.TypeArguments, root);
            }

            return type;
        }

        private INode Lookup(object o)
        {
            if (!this.initialised)
            {
                this.d[o] = this.n;
                this.initialised = true;
                return this.n;
            }

            if (!this.d.TryGetValue(o, out var current))
            {
                current = this.d[o] = this.n.Graph.CreateBlankNode();
            }

            return current;
        }

        private void AddType(INode type)
        {
            this.AddStatement(Vocabulary.RdfType, type);
        }

        private void AddStatement(INode p, Expression e)
        {
            this.AddStatement(p, this.Lookup(e));
        }

        private void AddStatement(INode p, string o)
        {
            this.AddStatement(p, new StringNode(this.Current.Graph, o));
        }

        private void AddStatement(INode p, INode o)
        {
            this.Current.Graph.Assert(this.Current, p, o);
        }
    }

    internal class ExpressionComparer : IEqualityComparer<Expression>
    {
        bool IEqualityComparer<Expression>.Equals(Expression x, Expression y) =>
            x.GetDebugView().GetHashCode(StringComparison.Ordinal) == y.GetDebugView().GetHashCode(StringComparison.Ordinal);

        int IEqualityComparer<Expression>.GetHashCode(Expression obj) =>
            obj.GetDebugView().GetHashCode(StringComparison.Ordinal);
    }
}
