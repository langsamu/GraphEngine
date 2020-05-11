// MIT License, Copyright 2020 Samu Lang

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

        public static ArrayLength Create(INode node) => new ArrayLength(node) { RdfType = Vocabulary.ArrayLength };
    }

    public class Convert : Unary
    {
        [DebuggerStepThrough]
        internal Convert(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Convert;

        public static Convert Create(INode node) => new Convert(node) { RdfType = Vocabulary.Convert };
    }

    public class ConvertChecked : Unary
    {
        [DebuggerStepThrough]
        internal ConvertChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.ConvertChecked;

        public static ConvertChecked Create(INode node) => new ConvertChecked(node) { RdfType = Vocabulary.ConvertChecked };
    }

    public class Decrement : Unary
    {
        [DebuggerStepThrough]
        internal Decrement(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Decrement;

        public static Decrement Create(INode node) => new Decrement(node) { RdfType = Vocabulary.Decrement };
    }

    public class Increment : Unary
    {
        [DebuggerStepThrough]
        internal Increment(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Increment;

        public static Increment Create(INode node) => new Increment(node) { RdfType = Vocabulary.Increment };
    }

    public class IsFalse : Unary
    {
        [DebuggerStepThrough]
        internal IsFalse(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.IsFalse;

        public static IsFalse Create(INode node) => new IsFalse(node) { RdfType = Vocabulary.IsFalse };
    }

    public class IsTrue : Unary
    {
        [DebuggerStepThrough]
        internal IsTrue(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.IsTrue;

        public static IsTrue Create(INode node) => new IsTrue(node) { RdfType = Vocabulary.IsTrue };
    }

    public class Negate : Unary
    {
        [DebuggerStepThrough]
        internal Negate(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Negate;

        public static Negate Create(INode node) => new Negate(node) { RdfType = Vocabulary.Negate };
    }

    public class NegateChecked : Unary
    {
        [DebuggerStepThrough]
        internal NegateChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.NegateChecked;

        public static NegateChecked Create(INode node) => new NegateChecked(node) { RdfType = Vocabulary.NegateChecked };
    }

    public class Not : Unary
    {
        [DebuggerStepThrough]
        internal Not(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Not;

        public static Not Create(INode node) => new Not(node) { RdfType = Vocabulary.Not };
    }

    public class OnesComplement : Unary
    {
        [DebuggerStepThrough]
        internal OnesComplement(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.OnesComplement;

        public static OnesComplement Create(INode node) => new OnesComplement(node) { RdfType = Vocabulary.OnesComplement };
    }

    public class PostDecrementAssign : Unary
    {
        [DebuggerStepThrough]
        internal PostDecrementAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.PostDecrementAssign;

        public static PostDecrementAssign Create(INode node) => new PostDecrementAssign(node) { RdfType = Vocabulary.PostDecrementAssign };
    }

    public class PostIncrementAssign : Unary
    {
        [DebuggerStepThrough]
        internal PostIncrementAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.PostIncrementAssign;

        public static PostIncrementAssign Create(INode node) => new PostIncrementAssign(node) { RdfType = Vocabulary.PostIncrementAssign };
    }

    public class PreDecrementAssign : Unary
    {
        [DebuggerStepThrough]
        internal PreDecrementAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.PreDecrementAssign;

        public static PreDecrementAssign Create(INode node) => new PreDecrementAssign(node) { RdfType = Vocabulary.PreDecrementAssign };
    }

    public class PreIncrementAssign : Unary
    {
        [DebuggerStepThrough]
        internal PreIncrementAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.PreIncrementAssign;

        public static PreIncrementAssign Create(INode node) => new PreIncrementAssign(node) { RdfType = Vocabulary.PreIncrementAssign };
    }

    public class Quote : Unary
    {
        [DebuggerStepThrough]
        internal Quote(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Quote;

        public static Quote Create(INode node) => new Quote(node) { RdfType = Vocabulary.Quote };
    }

    public class Throw : Unary
    {
        [DebuggerStepThrough]
        internal Throw(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Throw;

        public static Throw Create(INode node) => new Throw(node) { RdfType = Vocabulary.Throw };
    }

    public class TypeAs : Unary
    {
        [DebuggerStepThrough]
        internal TypeAs(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.TypeAs;

        public static TypeAs Create(INode node) => new TypeAs(node) { RdfType = Vocabulary.TypeAs };
    }

    public class UnaryPlus : Unary
    {
        [DebuggerStepThrough]
        internal UnaryPlus(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.UnaryPlus;

        public static UnaryPlus Create(INode node) => new UnaryPlus(node) { RdfType = Vocabulary.UnaryPlus };
    }

    public class Unbox : Unary
    {
        [DebuggerStepThrough]
        internal Unbox(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqUnaryType => Linq.ExpressionType.Unbox;

        public static Unbox Create(INode node) => new Unbox(node) { RdfType = Vocabulary.Unbox };
    }
}
