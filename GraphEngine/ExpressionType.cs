// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class ExpressionType : Node
    {
        [DebuggerStepThrough]
        public ExpressionType(NodeWithGraph node)
            : base(node)
        {
        }

        public Linq.ExpressionType LinqExpressionType => this switch
        {
            var n when n.Equals(Vocabulary.ExpressionTypes.Add) => Linq.ExpressionType.Add,
            var n when n.Equals(Vocabulary.ExpressionTypes.AddAssign) => Linq.ExpressionType.AddAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.AddAssignChecked) => Linq.ExpressionType.AddAssignChecked,
            var n when n.Equals(Vocabulary.ExpressionTypes.AddChecked) => Linq.ExpressionType.AddChecked,
            var n when n.Equals(Vocabulary.ExpressionTypes.And) => Linq.ExpressionType.And,
            var n when n.Equals(Vocabulary.ExpressionTypes.AndAlso) => Linq.ExpressionType.AndAlso,
            var n when n.Equals(Vocabulary.ExpressionTypes.AndAssign) => Linq.ExpressionType.AndAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.ArrayIndex) => Linq.ExpressionType.ArrayIndex,
            var n when n.Equals(Vocabulary.ExpressionTypes.ArrayLength) => Linq.ExpressionType.ArrayLength,
            var n when n.Equals(Vocabulary.ExpressionTypes.Assign) => Linq.ExpressionType.Assign,
            var n when n.Equals(Vocabulary.ExpressionTypes.Coalesce) => Linq.ExpressionType.Coalesce,
            var n when n.Equals(Vocabulary.ExpressionTypes.Convert) => Linq.ExpressionType.Convert,
            var n when n.Equals(Vocabulary.ExpressionTypes.ConvertChecked) => Linq.ExpressionType.ConvertChecked,
            var n when n.Equals(Vocabulary.ExpressionTypes.Decrement) => Linq.ExpressionType.Decrement,
            var n when n.Equals(Vocabulary.ExpressionTypes.Divide) => Linq.ExpressionType.Divide,
            var n when n.Equals(Vocabulary.ExpressionTypes.DivideAssign) => Linq.ExpressionType.DivideAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.Equal) => Linq.ExpressionType.Equal,
            var n when n.Equals(Vocabulary.ExpressionTypes.ExclusiveOr) => Linq.ExpressionType.ExclusiveOr,
            var n when n.Equals(Vocabulary.ExpressionTypes.ExclusiveOrAssign) => Linq.ExpressionType.ExclusiveOrAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.GreaterThan) => Linq.ExpressionType.GreaterThan,
            var n when n.Equals(Vocabulary.ExpressionTypes.GreaterThanOrEqual) => Linq.ExpressionType.GreaterThanOrEqual,
            var n when n.Equals(Vocabulary.ExpressionTypes.Increment) => Linq.ExpressionType.Increment,
            var n when n.Equals(Vocabulary.ExpressionTypes.IsFalse) => Linq.ExpressionType.IsFalse,
            var n when n.Equals(Vocabulary.ExpressionTypes.IsTrue) => Linq.ExpressionType.IsTrue,
            var n when n.Equals(Vocabulary.ExpressionTypes.LeftShift) => Linq.ExpressionType.LeftShift,
            var n when n.Equals(Vocabulary.ExpressionTypes.LeftShiftAssign) => Linq.ExpressionType.LeftShiftAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.LessThan) => Linq.ExpressionType.LessThan,
            var n when n.Equals(Vocabulary.ExpressionTypes.LessThanOrEqual) => Linq.ExpressionType.LessThanOrEqual,
            var n when n.Equals(Vocabulary.ExpressionTypes.Modulo) => Linq.ExpressionType.Modulo,
            var n when n.Equals(Vocabulary.ExpressionTypes.ModuloAssign) => Linq.ExpressionType.ModuloAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.Multiply) => Linq.ExpressionType.Multiply,
            var n when n.Equals(Vocabulary.ExpressionTypes.MultiplyAssign) => Linq.ExpressionType.MultiplyAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.MultiplyAssignChecked) => Linq.ExpressionType.MultiplyAssignChecked,
            var n when n.Equals(Vocabulary.ExpressionTypes.MultiplyChecked) => Linq.ExpressionType.MultiplyChecked,
            var n when n.Equals(Vocabulary.ExpressionTypes.Negate) => Linq.ExpressionType.Negate,
            var n when n.Equals(Vocabulary.ExpressionTypes.NegateChecked) => Linq.ExpressionType.NegateChecked,
            var n when n.Equals(Vocabulary.ExpressionTypes.Not) => Linq.ExpressionType.Not,
            var n when n.Equals(Vocabulary.ExpressionTypes.NotEqual) => Linq.ExpressionType.NotEqual,
            var n when n.Equals(Vocabulary.ExpressionTypes.OnesComplement) => Linq.ExpressionType.OnesComplement,
            var n when n.Equals(Vocabulary.ExpressionTypes.Or) => Linq.ExpressionType.Or,
            var n when n.Equals(Vocabulary.ExpressionTypes.OrAssign) => Linq.ExpressionType.OrAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.OrElse) => Linq.ExpressionType.OrElse,
            var n when n.Equals(Vocabulary.ExpressionTypes.PostDecrementAssign) => Linq.ExpressionType.PostDecrementAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.PostIncrementAssign) => Linq.ExpressionType.PostIncrementAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.Power) => Linq.ExpressionType.Power,
            var n when n.Equals(Vocabulary.ExpressionTypes.PowerAssign) => Linq.ExpressionType.PowerAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.PreDecrementAssign) => Linq.ExpressionType.PreDecrementAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.PreIncrementAssign) => Linq.ExpressionType.PreIncrementAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.Quote) => Linq.ExpressionType.Quote,
            var n when n.Equals(Vocabulary.ExpressionTypes.RightShift) => Linq.ExpressionType.RightShift,
            var n when n.Equals(Vocabulary.ExpressionTypes.RightShiftAssign) => Linq.ExpressionType.RightShiftAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.Subtract) => Linq.ExpressionType.Subtract,
            var n when n.Equals(Vocabulary.ExpressionTypes.SubtractAssign) => Linq.ExpressionType.SubtractAssign,
            var n when n.Equals(Vocabulary.ExpressionTypes.SubtractAssignChecked) => Linq.ExpressionType.SubtractAssignChecked,
            var n when n.Equals(Vocabulary.ExpressionTypes.SubtractChecked) => Linq.ExpressionType.SubtractChecked,
            var n when n.Equals(Vocabulary.ExpressionTypes.TypeAs) => Linq.ExpressionType.TypeAs,
            var n when n.Equals(Vocabulary.ExpressionTypes.TypeEqual) => Linq.ExpressionType.TypeEqual,
            var n when n.Equals(Vocabulary.ExpressionTypes.TypeIs) => Linq.ExpressionType.TypeIs,
            var n when n.Equals(Vocabulary.ExpressionTypes.UnaryPlus) => Linq.ExpressionType.UnaryPlus,
            var n when n.Equals(Vocabulary.ExpressionTypes.Unbox) => Linq.ExpressionType.Unbox,

            var unknown => throw new InvalidOperationException($"Unknown expression type node {unknown}"),
        };

        internal static ExpressionType Create(Linq.ExpressionType expressionType, IGraph graph) => Parse(new NodeWithGraph(
            expressionType switch
            {
                Linq.ExpressionType.Add => Vocabulary.ExpressionTypes.Add,
                Linq.ExpressionType.AddAssign => Vocabulary.ExpressionTypes.AddAssign,
                Linq.ExpressionType.AddAssignChecked => Vocabulary.ExpressionTypes.AddAssignChecked,
                Linq.ExpressionType.AddChecked => Vocabulary.ExpressionTypes.AddChecked,
                Linq.ExpressionType.And => Vocabulary.ExpressionTypes.And,
                Linq.ExpressionType.AndAlso => Vocabulary.ExpressionTypes.AndAlso,
                Linq.ExpressionType.AndAssign => Vocabulary.ExpressionTypes.AndAssign,
                Linq.ExpressionType.ArrayIndex => Vocabulary.ExpressionTypes.ArrayIndex,
                Linq.ExpressionType.ArrayLength => Vocabulary.ExpressionTypes.ArrayLength,
                Linq.ExpressionType.Assign => Vocabulary.ExpressionTypes.Assign,
                Linq.ExpressionType.Coalesce => Vocabulary.ExpressionTypes.Coalesce,
                Linq.ExpressionType.Convert => Vocabulary.ExpressionTypes.Convert,
                Linq.ExpressionType.ConvertChecked => Vocabulary.ExpressionTypes.ConvertChecked,
                Linq.ExpressionType.Decrement => Vocabulary.ExpressionTypes.Decrement,
                Linq.ExpressionType.Divide => Vocabulary.ExpressionTypes.Divide,
                Linq.ExpressionType.DivideAssign => Vocabulary.ExpressionTypes.DivideAssign,
                Linq.ExpressionType.Equal => Vocabulary.ExpressionTypes.Equal,
                Linq.ExpressionType.ExclusiveOr => Vocabulary.ExpressionTypes.ExclusiveOr,
                Linq.ExpressionType.ExclusiveOrAssign => Vocabulary.ExpressionTypes.ExclusiveOrAssign,
                Linq.ExpressionType.GreaterThan => Vocabulary.ExpressionTypes.GreaterThan,
                Linq.ExpressionType.GreaterThanOrEqual => Vocabulary.ExpressionTypes.GreaterThanOrEqual,
                Linq.ExpressionType.Increment => Vocabulary.ExpressionTypes.Increment,
                Linq.ExpressionType.IsFalse => Vocabulary.ExpressionTypes.IsFalse,
                Linq.ExpressionType.IsTrue => Vocabulary.ExpressionTypes.IsTrue,
                Linq.ExpressionType.LeftShift => Vocabulary.ExpressionTypes.LeftShift,
                Linq.ExpressionType.LeftShiftAssign => Vocabulary.ExpressionTypes.LeftShiftAssign,
                Linq.ExpressionType.LessThan => Vocabulary.ExpressionTypes.LessThan,
                Linq.ExpressionType.LessThanOrEqual => Vocabulary.ExpressionTypes.LessThanOrEqual,
                Linq.ExpressionType.Modulo => Vocabulary.ExpressionTypes.Modulo,
                Linq.ExpressionType.ModuloAssign => Vocabulary.ExpressionTypes.ModuloAssign,
                Linq.ExpressionType.Multiply => Vocabulary.ExpressionTypes.Multiply,
                Linq.ExpressionType.MultiplyAssign => Vocabulary.ExpressionTypes.MultiplyAssign,
                Linq.ExpressionType.MultiplyAssignChecked => Vocabulary.ExpressionTypes.MultiplyAssignChecked,
                Linq.ExpressionType.MultiplyChecked => Vocabulary.ExpressionTypes.MultiplyChecked,
                Linq.ExpressionType.Negate => Vocabulary.ExpressionTypes.Negate,
                Linq.ExpressionType.NegateChecked => Vocabulary.ExpressionTypes.NegateChecked,
                Linq.ExpressionType.Not => Vocabulary.ExpressionTypes.Not,
                Linq.ExpressionType.NotEqual => Vocabulary.ExpressionTypes.NotEqual,
                Linq.ExpressionType.OnesComplement => Vocabulary.ExpressionTypes.OnesComplement,
                Linq.ExpressionType.Or => Vocabulary.ExpressionTypes.Or,
                Linq.ExpressionType.OrAssign => Vocabulary.ExpressionTypes.OrAssign,
                Linq.ExpressionType.OrElse => Vocabulary.ExpressionTypes.OrElse,
                Linq.ExpressionType.PostDecrementAssign => Vocabulary.ExpressionTypes.PostDecrementAssign,
                Linq.ExpressionType.PostIncrementAssign => Vocabulary.ExpressionTypes.PostIncrementAssign,
                Linq.ExpressionType.Power => Vocabulary.ExpressionTypes.Power,
                Linq.ExpressionType.PowerAssign => Vocabulary.ExpressionTypes.PowerAssign,
                Linq.ExpressionType.PreDecrementAssign => Vocabulary.ExpressionTypes.PreDecrementAssign,
                Linq.ExpressionType.PreIncrementAssign => Vocabulary.ExpressionTypes.PreIncrementAssign,
                Linq.ExpressionType.Quote => Vocabulary.ExpressionTypes.Quote,
                Linq.ExpressionType.RightShift => Vocabulary.ExpressionTypes.RightShift,
                Linq.ExpressionType.RightShiftAssign => Vocabulary.ExpressionTypes.RightShiftAssign,
                Linq.ExpressionType.Subtract => Vocabulary.ExpressionTypes.Subtract,
                Linq.ExpressionType.SubtractAssign => Vocabulary.ExpressionTypes.SubtractAssign,
                Linq.ExpressionType.SubtractAssignChecked => Vocabulary.ExpressionTypes.SubtractAssignChecked,
                Linq.ExpressionType.SubtractChecked => Vocabulary.ExpressionTypes.SubtractChecked,
                Linq.ExpressionType.TypeAs => Vocabulary.ExpressionTypes.TypeAs,
                Linq.ExpressionType.TypeEqual => Vocabulary.ExpressionTypes.TypeEqual,
                Linq.ExpressionType.TypeIs => Vocabulary.ExpressionTypes.TypeIs,
                Linq.ExpressionType.UnaryPlus => Vocabulary.ExpressionTypes.UnaryPlus,
                Linq.ExpressionType.Unbox => Vocabulary.ExpressionTypes.Unbox,

                var unknown => throw new InvalidOperationException($"Uknown expression type {unknown}")
            }, graph));

        internal static ExpressionType Parse(NodeWithGraph node) => new (node);
    }
}
