// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class ArrayLength : Unary
    {
        [DebuggerStepThrough]
        internal ArrayLength(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.ArrayLength;
    }

    public class Convert : Unary
    {
        [DebuggerStepThrough]
        internal Convert(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Convert;
    }

    public class ConvertChecked : Unary
    {
        [DebuggerStepThrough]
        internal ConvertChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.ConvertChecked;
    }

    public class Decrement : Unary
    {
        [DebuggerStepThrough]
        internal Decrement(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Decrement;
    }

    public class Increment : Unary
    {
        [DebuggerStepThrough]
        internal Increment(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Increment;
    }

    public class IsFalse : Unary
    {
        [DebuggerStepThrough]
        internal IsFalse(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.IsFalse;
    }

    public class IsTrue : Unary
    {
        [DebuggerStepThrough]
        internal IsTrue(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.IsTrue;
    }

    public class Negate : Unary
    {
        [DebuggerStepThrough]
        internal Negate(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Negate;
    }

    public class NegateChecked : Unary
    {
        [DebuggerStepThrough]
        internal NegateChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.NegateChecked;
    }

    public class Not : Unary
    {
        [DebuggerStepThrough]
        internal Not(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Not;
    }

    public class OnesComplement : Unary
    {
        [DebuggerStepThrough]
        internal OnesComplement(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.OnesComplement;
    }

    public class PostDecrementAssign : Unary
    {
        [DebuggerStepThrough]
        internal PostDecrementAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.PostDecrementAssign;
    }

    public class PostIncrementAssign : Unary
    {
        [DebuggerStepThrough]
        internal PostIncrementAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.PostIncrementAssign;
    }

    public class PreDecrementAssign : Unary
    {
        [DebuggerStepThrough]
        internal PreDecrementAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.PreDecrementAssign;
    }

    public class PreIncrementAssign : Unary
    {
        [DebuggerStepThrough]
        internal PreIncrementAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.PreIncrementAssign;
    }

    public class Quote : Unary
    {
        [DebuggerStepThrough]
        internal Quote(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Quote;
    }

    public class Throw : Unary
    {
        [DebuggerStepThrough]
        internal Throw(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Throw;
    }

    public class TypeAs : Unary
    {
        [DebuggerStepThrough]
        internal TypeAs(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.TypeAs;
    }

    public class UnaryPlus : Unary
    {
        [DebuggerStepThrough]
        internal UnaryPlus(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.UnaryPlus;
    }

    public class Unbox : Unary
    {
        [DebuggerStepThrough]
        internal Unbox(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Unbox;
    }
}
