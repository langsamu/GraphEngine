// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Add : Binary
    {
        [DebuggerStepThrough]
        internal Add(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Add;

        public static Add Create(INode node) => new Add(node) { RdfType = Vocabulary.Add };
    }

    public class AddAssign : Binary
    {
        [DebuggerStepThrough]
        internal AddAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.AddAssign;

        public static AddAssign Create(INode node) => new AddAssign(node) { RdfType = Vocabulary.AddAssign };
    }

    public class AddAssignChecked : Binary
    {
        [DebuggerStepThrough]
        internal AddAssignChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.AddAssignChecked;

        public static AddAssignChecked Create(INode node) => new AddAssignChecked(node) { RdfType = Vocabulary.AddAssignChecked };
    }

    public class AddChecked : Binary
    {
        [DebuggerStepThrough]
        internal AddChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.AddChecked;

        public static AddChecked Create(INode node) => new AddChecked(node) { RdfType = Vocabulary.AddChecked };
    }

    public class And : Binary
    {
        [DebuggerStepThrough]
        internal And(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.And;

        public static And Create(INode node) => new And(node) { RdfType = Vocabulary.And };
    }

    public class AndAlso : Binary
    {
        [DebuggerStepThrough]
        internal AndAlso(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.AndAlso;

        public static AndAlso Create(INode node) => new AndAlso(node) { RdfType = Vocabulary.AndAlso };
    }

    public class AndAssign : Binary
    {
        [DebuggerStepThrough]
        internal AndAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.AndAssign;

        public static AndAssign Create(INode node) => new AndAssign(node) { RdfType = Vocabulary.AndAssign };
    }

    public class Assign : Binary
    {
        [DebuggerStepThrough]
        internal Assign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Assign;

        public static Assign Create(INode node) => new Assign(node) { RdfType = Vocabulary.Assign };
    }

    public class Coalesce : Binary
    {
        [DebuggerStepThrough]
        internal Coalesce(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Coalesce;

        public static Coalesce Create(INode node) => new Coalesce(node) { RdfType = Vocabulary.Coalesce };
    }

    public class Divide : Binary
    {
        [DebuggerStepThrough]
        internal Divide(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Divide;

        public static Divide Create(INode node) => new Divide(node) { RdfType = Vocabulary.Divide };
    }

    public class DivideAssign : Binary
    {
        [DebuggerStepThrough]
        internal DivideAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.DivideAssign;

        public static DivideAssign Create(INode node) => new DivideAssign(node) { RdfType = Vocabulary.DivideAssign };
    }

    public class Equal : Binary
    {
        [DebuggerStepThrough]
        internal Equal(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Equal;

        public static Equal Create(INode node) => new Equal(node) { RdfType = Vocabulary.Equal };
    }

    public class ExclusiveOr : Binary
    {
        [DebuggerStepThrough]
        internal ExclusiveOr(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.ExclusiveOr;

        public static ExclusiveOr Create(INode node) => new ExclusiveOr(node) { RdfType = Vocabulary.ExclusiveOr };
    }

    public class ExclusiveOrAssign : Binary
    {
        [DebuggerStepThrough]
        internal ExclusiveOrAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.ExclusiveOrAssign;

        public static ExclusiveOrAssign Create(INode node) => new ExclusiveOrAssign(node) { RdfType = Vocabulary.ExclusiveOrAssign };
    }

    public class GreaterThan : Binary
    {
        [DebuggerStepThrough]
        internal GreaterThan(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.GreaterThan;

        public static GreaterThan Create(INode node) => new GreaterThan(node) { RdfType = Vocabulary.GreaterThan };
    }

    public class GreaterThanOrEqual : Binary
    {
        [DebuggerStepThrough]
        internal GreaterThanOrEqual(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.GreaterThanOrEqual;

        public static GreaterThanOrEqual Create(INode node) => new GreaterThanOrEqual(node) { RdfType = Vocabulary.GreaterThanOrEqual };
    }

    public class LeftShift : Binary
    {
        [DebuggerStepThrough]
        internal LeftShift(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.LeftShift;

        public static LeftShift Create(INode node) => new LeftShift(node) { RdfType = Vocabulary.LeftShift };
    }

    public class LeftShiftAssign : Binary
    {
        [DebuggerStepThrough]
        internal LeftShiftAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.LeftShiftAssign;

        public static LeftShiftAssign Create(INode node) => new LeftShiftAssign(node) { RdfType = Vocabulary.LeftShiftAssign };
    }

    public class LessThan : Binary
    {
        [DebuggerStepThrough]
        internal LessThan(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.LessThan;

        public static LessThan Create(INode node) => new LessThan(node) { RdfType = Vocabulary.LessThan };
    }

    public class LessThanOrEqual : Binary
    {
        [DebuggerStepThrough]
        internal LessThanOrEqual(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.LessThanOrEqual;

        public static LessThanOrEqual Create(INode node) => new LessThanOrEqual(node) { RdfType = Vocabulary.LessThanOrEqual };
    }

    public class Modulo : Binary
    {
        [DebuggerStepThrough]
        internal Modulo(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Modulo;

        public static Modulo Create(INode node) => new Modulo(node) { RdfType = Vocabulary.Modulo };
    }

    public class ModuloAssign : Binary
    {
        [DebuggerStepThrough]
        internal ModuloAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.ModuloAssign;

        public static ModuloAssign Create(INode node) => new ModuloAssign(node) { RdfType = Vocabulary.ModuloAssign };
    }

    public class Multiply : Binary
    {
        [DebuggerStepThrough]
        internal Multiply(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Multiply;

        public static Multiply Create(INode node) => new Multiply(node) { RdfType = Vocabulary.Multiply };
    }

    public class MultiplyAssign : Binary
    {
        [DebuggerStepThrough]
        internal MultiplyAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.MultiplyAssign;

        public static MultiplyAssign Create(INode node) => new MultiplyAssign(node) { RdfType = Vocabulary.MultiplyAssign };
    }

    public class MultiplyAssignChecked : Binary
    {
        [DebuggerStepThrough]
        internal MultiplyAssignChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.MultiplyAssignChecked;

        public static MultiplyAssignChecked Create(INode node) => new MultiplyAssignChecked(node) { RdfType = Vocabulary.MultiplyAssignChecked };
    }

    public class MultiplyChecked : Binary
    {
        [DebuggerStepThrough]
        internal MultiplyChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.MultiplyChecked;

        public static MultiplyChecked Create(INode node) => new MultiplyChecked(node) { RdfType = Vocabulary.MultiplyChecked };
    }

    public class NotEqual : Binary
    {
        [DebuggerStepThrough]
        internal NotEqual(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.NotEqual;

        public static NotEqual Create(INode node) => new NotEqual(node) { RdfType = Vocabulary.NotEqual };
    }

    public class Or : Binary
    {
        [DebuggerStepThrough]
        internal Or(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Or;

        public static Or Create(INode node) => new Or(node) { RdfType = Vocabulary.Or };
    }

    public class OrAssign : Binary
    {
        [DebuggerStepThrough]
        internal OrAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.OrAssign;

        public static OrAssign Create(INode node) => new OrAssign(node) { RdfType = Vocabulary.OrAssign };
    }

    public class OrElse : Binary
    {
        [DebuggerStepThrough]
        internal OrElse(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.OrElse;

        public static OrElse Create(INode node) => new OrElse(node) { RdfType = Vocabulary.OrElse };
    }

    public class Power : Binary
    {
        [DebuggerStepThrough]
        internal Power(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Power;

        public static Power Create(INode node) => new Power(node) { RdfType = Vocabulary.Power };
    }

    public class PowerAssign : Binary
    {
        [DebuggerStepThrough]
        internal PowerAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.PowerAssign;

        public static PowerAssign Create(INode node) => new PowerAssign(node) { RdfType = Vocabulary.PowerAssign };
    }

    public class RightShift : Binary
    {
        [DebuggerStepThrough]
        internal RightShift(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.RightShift;

        public static RightShift Create(INode node) => new RightShift(node) { RdfType = Vocabulary.RightShift };
    }

    public class RightShiftAssign : Binary
    {
        [DebuggerStepThrough]
        internal RightShiftAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.RightShiftAssign;

        public static RightShiftAssign Create(INode node) => new RightShiftAssign(node) { RdfType = Vocabulary.RightShiftAssign };
    }

    public class Subtract : Binary
    {
        [DebuggerStepThrough]
        internal Subtract(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.Subtract;

        public static Subtract Create(INode node) => new Subtract(node) { RdfType = Vocabulary.Subtract };
    }

    public class SubtractAssign : Binary
    {
        [DebuggerStepThrough]
        internal SubtractAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.SubtractAssign;

        public static SubtractAssign Create(INode node) => new SubtractAssign(node) { RdfType = Vocabulary.SubtractAssign };
    }

    public class SubtractAssignChecked : Binary
    {
        [DebuggerStepThrough]
        internal SubtractAssignChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.SubtractAssignChecked;

        public static SubtractAssignChecked Create(INode node) => new SubtractAssignChecked(node) { RdfType = Vocabulary.SubtractAssignChecked };
    }

    public class SubtractChecked : Binary
    {
        [DebuggerStepThrough]
        internal SubtractChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType LinqBinaryType => Linq.ExpressionType.SubtractChecked;

        public static SubtractChecked Create(INode node) => new SubtractChecked(node) { RdfType = Vocabulary.SubtractChecked };
    }
}
