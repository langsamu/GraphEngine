// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using VDS.RDF;

    internal static class Extensions
    {
        internal static IEnumerable<INode> ObjectsOf(this INode predicate, INode subject) =>
            from t in subject.Graph.GetTriplesWithSubjectPredicate(subject, predicate)
            select t.Object;

        internal static INode ObjectOf(this INode predicate, INode subject) =>
            predicate.ObjectsOf(subject).SingleOrDefault();

        internal static string GetDebugView(this Expression exp) =>
            (string)typeof(Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(exp);

        internal static INode AsNode(this ExpressionType type) =>
            type switch
            {
                ExpressionType.Add => Vocabulary.Add,
                ExpressionType.AddAssign => Vocabulary.AddAssign,
                ExpressionType.AddAssignChecked => Vocabulary.AddAssignChecked,
                ExpressionType.AddChecked => Vocabulary.AddChecked,
                ExpressionType.And => Vocabulary.And,
                ExpressionType.AndAlso => Vocabulary.AndAlso,
                ExpressionType.AndAssign => Vocabulary.AndAssign,
                ExpressionType.ArrayIndex => Vocabulary.ArrayIndex,
                ExpressionType.ArrayLength => Vocabulary.ArrayLength,
                ExpressionType.Assign => Vocabulary.Assign,
                ExpressionType.Block => Vocabulary.Block,
                ExpressionType.Call => Vocabulary.Call,
                ExpressionType.Coalesce => Vocabulary.Coalesce,
                ExpressionType.Constant => Vocabulary.Constant,
                ExpressionType.Convert => Vocabulary.Convert,
                ExpressionType.ConvertChecked => Vocabulary.ConvertChecked,
                ExpressionType.Decrement => Vocabulary.Decrement,
                ExpressionType.Default => Vocabulary.Default,
                ExpressionType.Divide => Vocabulary.Divide,
                ExpressionType.DivideAssign => Vocabulary.DivideAssign,
                ExpressionType.Equal => Vocabulary.Equal,
                ExpressionType.ExclusiveOr => Vocabulary.ExclusiveOr,
                ExpressionType.ExclusiveOrAssign => Vocabulary.ExclusiveOrAssign,
                ExpressionType.Goto => Vocabulary.Goto,
                ExpressionType.GreaterThan => Vocabulary.GreaterThan,
                ExpressionType.GreaterThanOrEqual => Vocabulary.GreaterThanOrEqual,
                ExpressionType.Increment => Vocabulary.Increment,
                ExpressionType.Invoke => Vocabulary.Invoke,
                ExpressionType.IsFalse => Vocabulary.IsFalse,
                ExpressionType.IsTrue => Vocabulary.IsTrue,
                ExpressionType.Lambda => Vocabulary.Lambda,
                ExpressionType.LeftShift => Vocabulary.LeftShift,
                ExpressionType.LeftShiftAssign => Vocabulary.LeftShiftAssign,
                ExpressionType.LessThan => Vocabulary.LessThan,
                ExpressionType.LessThanOrEqual => Vocabulary.LessThanOrEqual,
                ExpressionType.Loop => Vocabulary.Loop,
                ExpressionType.Modulo => Vocabulary.Modulo,
                ExpressionType.ModuloAssign => Vocabulary.ModuloAssign,
                ExpressionType.Multiply => Vocabulary.Multiply,
                ExpressionType.MultiplyAssign => Vocabulary.MultiplyAssign,
                ExpressionType.MultiplyAssignChecked => Vocabulary.MultiplyAssignChecked,
                ExpressionType.MultiplyChecked => Vocabulary.MultiplyChecked,
                ExpressionType.Negate => Vocabulary.Negate,
                ExpressionType.NegateChecked => Vocabulary.NegateChecked,
                ExpressionType.New => Vocabulary.New,
                ExpressionType.NewArrayBounds => Vocabulary.NewArrayBounds,
                ExpressionType.Not => Vocabulary.Not,
                ExpressionType.NotEqual => Vocabulary.NotEqual,
                ExpressionType.OnesComplement => Vocabulary.OnesComplement,
                ExpressionType.Or => Vocabulary.Or,
                ExpressionType.OrAssign => Vocabulary.OrAssign,
                ExpressionType.OrElse => Vocabulary.OrElse,
                ExpressionType.Parameter => Vocabulary.Parameter,
                ExpressionType.PostDecrementAssign => Vocabulary.PostDecrementAssign,
                ExpressionType.PostIncrementAssign => Vocabulary.PostIncrementAssign,
                ExpressionType.Power => Vocabulary.Power,
                ExpressionType.PowerAssign => Vocabulary.PowerAssign,
                ExpressionType.PreDecrementAssign => Vocabulary.PreDecrementAssign,
                ExpressionType.PreIncrementAssign => Vocabulary.PreIncrementAssign,
                ExpressionType.Quote => Vocabulary.Quote,
                ExpressionType.RightShift => Vocabulary.RightShift,
                ExpressionType.RightShiftAssign => Vocabulary.RightShiftAssign,
                ExpressionType.Subtract => Vocabulary.Subtract,
                ExpressionType.SubtractAssign => Vocabulary.SubtractAssign,
                ExpressionType.SubtractAssignChecked => Vocabulary.SubtractAssignChecked,
                ExpressionType.SubtractChecked => Vocabulary.SubtractChecked,
                ExpressionType.Throw => Vocabulary.Throw,
                ExpressionType.Try => Vocabulary.Try,
                ExpressionType.TypeAs => Vocabulary.TypeAs,
                ExpressionType.UnaryPlus => Vocabulary.UnaryPlus,
                ExpressionType.Unbox => Vocabulary.Unbox,

                _ => throw new NotImplementedException()
            };

        internal static INode AsNode(this GotoExpressionKind kind) =>
            kind switch
            {
                GotoExpressionKind.Break => Vocabulary.Break,
                GotoExpressionKind.Continue => Vocabulary.Continue,
                GotoExpressionKind.Goto => Vocabulary.Goto,
                GotoExpressionKind.Return => Vocabulary.Return,

                _ => throw new NotImplementedException()
            };
    }
}
