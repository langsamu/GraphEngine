// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Add : Binary
    {
        [DebuggerStepThrough]
        internal Add(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Add;
    }

    public class AddAssign : Binary
    {
        [DebuggerStepThrough]
        internal AddAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.AddAssign;
    }

    public class AddAssignChecked : Binary
    {
        [DebuggerStepThrough]
        internal AddAssignChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.AddAssignChecked;
    }

    public class AddChecked : Binary
    {
        [DebuggerStepThrough]
        internal AddChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.AddChecked;
    }

    public class And : Binary
    {
        [DebuggerStepThrough]
        internal And(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.And;
    }

    public class AndAlso : Binary
    {
        [DebuggerStepThrough]
        internal AndAlso(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.AndAlso;
    }

    public class AndAssign : Binary
    {
        [DebuggerStepThrough]
        internal AndAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.AndAssign;
    }

    public class Assign : Binary
    {
        [DebuggerStepThrough]
        internal Assign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Assign;
    }

    public class Coalesce : Binary
    {
        [DebuggerStepThrough]
        internal Coalesce(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Coalesce;
    }

    public class Divide : Binary
    {
        [DebuggerStepThrough]
        internal Divide(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Divide;
    }

    public class DivideAssign : Binary
    {
        [DebuggerStepThrough]
        internal DivideAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.DivideAssign;
    }

    public class Equal : Binary
    {
        [DebuggerStepThrough]
        internal Equal(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Equal;
    }

    public class ExclusiveOr : Binary
    {
        [DebuggerStepThrough]
        internal ExclusiveOr(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.ExclusiveOr;
    }

    public class ExclusiveOrAssign : Binary
    {
        [DebuggerStepThrough]
        internal ExclusiveOrAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.ExclusiveOrAssign;
    }

    public class GreaterThan : Binary
    {
        [DebuggerStepThrough]
        internal GreaterThan(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.GreaterThan;
    }

    public class GreaterThanOrEqual : Binary
    {
        [DebuggerStepThrough]
        internal GreaterThanOrEqual(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.GreaterThanOrEqual;
    }

    public class LeftShift : Binary
    {
        [DebuggerStepThrough]
        internal LeftShift(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.LeftShift;
    }

    public class LeftShiftAssign : Binary
    {
        [DebuggerStepThrough]
        internal LeftShiftAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.LeftShiftAssign;
    }

    public class LessThan : Binary
    {
        [DebuggerStepThrough]
        internal LessThan(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.LessThan;
    }

    public class LessThanOrEqual : Binary
    {
        [DebuggerStepThrough]
        internal LessThanOrEqual(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.LessThanOrEqual;
    }

    public class Modulo : Binary
    {
        [DebuggerStepThrough]
        internal Modulo(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Modulo;
    }

    public class ModuloAssign : Binary
    {
        [DebuggerStepThrough]
        internal ModuloAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.ModuloAssign;
    }

    public class Multiply : Binary
    {
        [DebuggerStepThrough]
        internal Multiply(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Multiply;
    }

    public class MultiplyAssign : Binary
    {
        [DebuggerStepThrough]
        internal MultiplyAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.MultiplyAssign;
    }

    public class MultiplyAssignChecked : Binary
    {
        [DebuggerStepThrough]
        internal MultiplyAssignChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.MultiplyAssignChecked;
    }

    public class MultiplyChecked : Binary
    {
        [DebuggerStepThrough]
        internal MultiplyChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.MultiplyChecked;
    }

    public class NotEqual : Binary
    {
        [DebuggerStepThrough]
        internal NotEqual(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.NotEqual;
    }

    public class Or : Binary
    {
        [DebuggerStepThrough]
        internal Or(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Or;
    }

    public class OrAssign : Binary
    {
        [DebuggerStepThrough]
        internal OrAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.OrAssign;
    }

    public class OrElse : Binary
    {
        [DebuggerStepThrough]
        internal OrElse(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.OrElse;
    }

    public class Power : Binary
    {
        [DebuggerStepThrough]
        internal Power(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Power;
    }

    public class PowerAssign : Binary
    {
        [DebuggerStepThrough]
        internal PowerAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.PowerAssign;
    }

    public class RightShift : Binary
    {
        [DebuggerStepThrough]
        internal RightShift(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.RightShift;
    }

    public class RightShiftAssign : Binary
    {
        [DebuggerStepThrough]
        internal RightShiftAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.RightShiftAssign;
    }

    public class Subtract : Binary
    {
        [DebuggerStepThrough]
        internal Subtract(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.Subtract;
    }

    public class SubtractAssign : Binary
    {
        [DebuggerStepThrough]
        internal SubtractAssign(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.SubtractAssign;
    }

    public class SubtractAssignChecked : Binary
    {
        [DebuggerStepThrough]
        internal SubtractAssignChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.SubtractAssignChecked;
    }

    public class SubtractChecked : Binary
    {
        [DebuggerStepThrough]
        internal SubtractChecked(INode node) : base(node) { }

        protected override Linq.ExpressionType BinaryType => Linq.ExpressionType.SubtractChecked;
    }
}
