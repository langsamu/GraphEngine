// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

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

        public override Expression Visit(Expression node)
        {
            this.path.Push(this.Lookup(node));
            var a = base.Visit(node);
            this.path.Pop();
            return a;
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            this.AssertType(Vocabulary.Block);

            if (node.Variables.Any())
            {
                var currentNode = this.path.Peek();
                var root = currentNode.Graph.AssertList(node.Variables.Select(this.Lookup));
                currentNode.Graph.Assert(currentNode, Vocabulary.BlockVariables, root);
            }

            if (node.Expressions.Any())
            {
                var currentNode = this.path.Peek();
                var root = currentNode.Graph.AssertList(node.Expressions.Select(this.Lookup));
                currentNode.Graph.Assert(currentNode, Vocabulary.BlockExpressions, root);
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

            this.AssertType(type);

            var currentNode = this.path.Peek();
            currentNode.Graph.Assert(currentNode, Vocabulary.BinaryLeft, this.Lookup(node.Left));
            currentNode.Graph.Assert(currentNode, Vocabulary.BinaryRight, this.Lookup(node.Right));

            return base.VisitBinary(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            this.AssertType(Vocabulary.Parameter);

            // TODO: Type
            // TODO: Name

            return base.VisitParameter(node);
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            this.AssertType(Vocabulary.Loop);

            var currentNode = this.path.Peek();
            currentNode.Graph.Assert(currentNode, Vocabulary.LoopBody, this.Lookup(node.Body));

            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.LoopBreak, this.Lookup(node.BreakLabel));
            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.LoopContinue, this.Lookup(node.ContinueLabel));

            return base.VisitLoop(node);
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

            this.AssertType(type);

            var currentNode = this.path.Peek();
            currentNode.Graph.Assert(currentNode, Vocabulary.UnaryOperand, this.Lookup(node.Operand));
            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.UnaryType, this.Lookup(node.Type));

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

            this.AssertType(type);

            var currentNode = this.path.Peek();
            currentNode.Graph.Assert(currentNode, Vocabulary.GotoValue, this.Lookup(node.Value));
            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.GotoTarget, this.Lookup(node.Target));
            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.GotoType, this.Lookup(node.Type));

            return base.VisitGoto(node);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            this.AssertType(Vocabulary.Condition);

            var currentNode = this.path.Peek();
            currentNode.Graph.Assert(currentNode, Vocabulary.ConditionIfFalse, this.Lookup(node.IfFalse));
            currentNode.Graph.Assert(currentNode, Vocabulary.ConditionIfTrue, this.Lookup(node.IfTrue));
            currentNode.Graph.Assert(currentNode, Vocabulary.ConditionTest, this.Lookup(node.Test));
            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.ConditionType, this.Lookup(node.Type));

            return base.VisitConditional(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            this.AssertType(Vocabulary.Constant);

            var currentNode = this.path.Peek();
            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.ConstantValue, this.Lookup(node.Value));
            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.ConstantType, this.Lookup(node.Type));

            return base.VisitConstant(node);
        }

        private INode Lookup(Expression e)
        {
            if (!this.initialised)
            {
                this.d[e] = this.n;
                this.initialised = true;
                return this.n;
            }

            if (!this.d.TryGetValue(e, out var current))
            {
                current = this.d[e] = this.n.Graph.CreateBlankNode();
            }

            return current;
        }

        private void AssertType(IUriNode type)
        {
            var currentNode = this.path.Peek();
            currentNode.Graph.Assert(currentNode, Vocabulary.RdfType, type);
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
