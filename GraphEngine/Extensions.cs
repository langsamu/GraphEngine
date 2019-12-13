// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    internal static class Extensions
    {
        internal static IEnumerable<INode> ObjectsOf(this INode predicate, INode subject) =>
            from t in subject.Graph.GetTriplesWithSubjectPredicate(subject, predicate)
            select t.Object;

        internal static INode ObjectOf(this INode predicate, INode subject) =>
            predicate.ObjectsOf(subject).SingleOrDefault();

        internal static string GetDebugView(this Linq.Expression exp) =>
            (string)typeof(Linq.Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(exp);

        internal static INode AsNode(this Linq.ExpressionType type) =>
            type switch
            {
                Linq.ExpressionType.Add => Vocabulary.Add,
                Linq.ExpressionType.AddAssign => Vocabulary.AddAssign,
                Linq.ExpressionType.AddAssignChecked => Vocabulary.AddAssignChecked,
                Linq.ExpressionType.AddChecked => Vocabulary.AddChecked,
                Linq.ExpressionType.And => Vocabulary.And,
                Linq.ExpressionType.AndAlso => Vocabulary.AndAlso,
                Linq.ExpressionType.AndAssign => Vocabulary.AndAssign,
                Linq.ExpressionType.ArrayIndex => Vocabulary.ArrayIndex,
                Linq.ExpressionType.ArrayLength => Vocabulary.ArrayLength,
                Linq.ExpressionType.Assign => Vocabulary.Assign,
                Linq.ExpressionType.Block => Vocabulary.Block,
                Linq.ExpressionType.Call => Vocabulary.Call,
                Linq.ExpressionType.Coalesce => Vocabulary.Coalesce,
                Linq.ExpressionType.Constant => Vocabulary.Constant,
                Linq.ExpressionType.Convert => Vocabulary.Convert,
                Linq.ExpressionType.ConvertChecked => Vocabulary.ConvertChecked,
                Linq.ExpressionType.Decrement => Vocabulary.Decrement,
                Linq.ExpressionType.Default => Vocabulary.Default,
                Linq.ExpressionType.Divide => Vocabulary.Divide,
                Linq.ExpressionType.DivideAssign => Vocabulary.DivideAssign,
                Linq.ExpressionType.Equal => Vocabulary.Equal,
                Linq.ExpressionType.ExclusiveOr => Vocabulary.ExclusiveOr,
                Linq.ExpressionType.ExclusiveOrAssign => Vocabulary.ExclusiveOrAssign,
                Linq.ExpressionType.Goto => Vocabulary.Goto,
                Linq.ExpressionType.GreaterThan => Vocabulary.GreaterThan,
                Linq.ExpressionType.GreaterThanOrEqual => Vocabulary.GreaterThanOrEqual,
                Linq.ExpressionType.Increment => Vocabulary.Increment,
                Linq.ExpressionType.Invoke => Vocabulary.Invoke,
                Linq.ExpressionType.IsFalse => Vocabulary.IsFalse,
                Linq.ExpressionType.IsTrue => Vocabulary.IsTrue,
                Linq.ExpressionType.Lambda => Vocabulary.Lambda,
                Linq.ExpressionType.LeftShift => Vocabulary.LeftShift,
                Linq.ExpressionType.LeftShiftAssign => Vocabulary.LeftShiftAssign,
                Linq.ExpressionType.LessThan => Vocabulary.LessThan,
                Linq.ExpressionType.LessThanOrEqual => Vocabulary.LessThanOrEqual,
                Linq.ExpressionType.Loop => Vocabulary.Loop,
                Linq.ExpressionType.Modulo => Vocabulary.Modulo,
                Linq.ExpressionType.ModuloAssign => Vocabulary.ModuloAssign,
                Linq.ExpressionType.Multiply => Vocabulary.Multiply,
                Linq.ExpressionType.MultiplyAssign => Vocabulary.MultiplyAssign,
                Linq.ExpressionType.MultiplyAssignChecked => Vocabulary.MultiplyAssignChecked,
                Linq.ExpressionType.MultiplyChecked => Vocabulary.MultiplyChecked,
                Linq.ExpressionType.Negate => Vocabulary.Negate,
                Linq.ExpressionType.NegateChecked => Vocabulary.NegateChecked,
                Linq.ExpressionType.New => Vocabulary.New,
                Linq.ExpressionType.NewArrayBounds => Vocabulary.NewArrayBounds,
                Linq.ExpressionType.Not => Vocabulary.Not,
                Linq.ExpressionType.NotEqual => Vocabulary.NotEqual,
                Linq.ExpressionType.OnesComplement => Vocabulary.OnesComplement,
                Linq.ExpressionType.Or => Vocabulary.Or,
                Linq.ExpressionType.OrAssign => Vocabulary.OrAssign,
                Linq.ExpressionType.OrElse => Vocabulary.OrElse,
                Linq.ExpressionType.Parameter => Vocabulary.Parameter,
                Linq.ExpressionType.PostDecrementAssign => Vocabulary.PostDecrementAssign,
                Linq.ExpressionType.PostIncrementAssign => Vocabulary.PostIncrementAssign,
                Linq.ExpressionType.Power => Vocabulary.Power,
                Linq.ExpressionType.PowerAssign => Vocabulary.PowerAssign,
                Linq.ExpressionType.PreDecrementAssign => Vocabulary.PreDecrementAssign,
                Linq.ExpressionType.PreIncrementAssign => Vocabulary.PreIncrementAssign,
                Linq.ExpressionType.Quote => Vocabulary.Quote,
                Linq.ExpressionType.RightShift => Vocabulary.RightShift,
                Linq.ExpressionType.RightShiftAssign => Vocabulary.RightShiftAssign,
                Linq.ExpressionType.Subtract => Vocabulary.Subtract,
                Linq.ExpressionType.SubtractAssign => Vocabulary.SubtractAssign,
                Linq.ExpressionType.SubtractAssignChecked => Vocabulary.SubtractAssignChecked,
                Linq.ExpressionType.SubtractChecked => Vocabulary.SubtractChecked,
                Linq.ExpressionType.Throw => Vocabulary.Throw,
                Linq.ExpressionType.Try => Vocabulary.Try,
                Linq.ExpressionType.TypeAs => Vocabulary.TypeAs,
                Linq.ExpressionType.UnaryPlus => Vocabulary.UnaryPlus,
                Linq.ExpressionType.Unbox => Vocabulary.Unbox,

                _ => throw new NotImplementedException()
            };

        internal static INode AsNode(this Linq.GotoExpressionKind kind) =>
            kind switch
            {
                Linq.GotoExpressionKind.Break => Vocabulary.Break,
                Linq.GotoExpressionKind.Continue => Vocabulary.Continue,
                Linq.GotoExpressionKind.Goto => Vocabulary.Goto,
                Linq.GotoExpressionKind.Return => Vocabulary.Return,

                _ => throw new NotImplementedException()
            };
    }
}
