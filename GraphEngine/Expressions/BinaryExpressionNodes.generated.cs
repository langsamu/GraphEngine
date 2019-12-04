namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class AddExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AddExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Add;
    }

    public class AddAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AddAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AddAssign;
    }

    public class AddAssignCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AddAssignCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AddAssignChecked;
    }

    public class AddCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AddCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AddChecked;
    }

    public class AndExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AndExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.And;
    }

    public class AndAlsoExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AndAlsoExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AndAlso;
    }

    public class AndAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AndAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AndAssign;
    }

    public class ArrayIndexExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal ArrayIndexExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.ArrayIndex;
    }

    public class AssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Assign;
    }

    public class CoalesceExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal CoalesceExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Coalesce;
    }

    public class DivideExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal DivideExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Divide;
    }

    public class DivideAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal DivideAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.DivideAssign;
    }

    public class EqualExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal EqualExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Equal;
    }

    public class ExclusiveOrExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal ExclusiveOrExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.ExclusiveOr;
    }

    public class ExclusiveOrAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal ExclusiveOrAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.ExclusiveOrAssign;
    }

    public class GreaterThanExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal GreaterThanExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.GreaterThan;
    }

    public class GreaterThanOrEqualExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal GreaterThanOrEqualExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.GreaterThanOrEqual;
    }

    public class LeftShiftExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal LeftShiftExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.LeftShift;
    }

    public class LeftShiftAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal LeftShiftAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.LeftShiftAssign;
    }

    public class LessThanExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal LessThanExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.LessThan;
    }

    public class LessThanOrEqualExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal LessThanOrEqualExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.LessThanOrEqual;
    }

    public class ModuloExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal ModuloExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Modulo;
    }

    public class ModuloAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal ModuloAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.ModuloAssign;
    }

    public class MultiplyExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal MultiplyExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Multiply;
    }

    public class MultiplyAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal MultiplyAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.MultiplyAssign;
    }

    public class MultiplyAssignCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal MultiplyAssignCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.MultiplyAssignChecked;
    }

    public class MultiplyCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal MultiplyCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.MultiplyChecked;
    }

    public class NotEqualExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal NotEqualExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.NotEqual;
    }

    public class OrExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal OrExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Or;
    }

    public class OrAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal OrAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.OrAssign;
    }

    public class OrElseExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal OrElseExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.OrElse;
    }

    public class PowerExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal PowerExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Power;
    }

    public class PowerAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal PowerAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.PowerAssign;
    }

    public class RightShiftExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal RightShiftExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.RightShift;
    }

    public class RightShiftAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal RightShiftAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.RightShiftAssign;
    }

    public class SubtractExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal SubtractExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Subtract;
    }

    public class SubtractAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal SubtractAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.SubtractAssign;
    }

    public class SubtractAssignCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal SubtractAssignCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.SubtractAssignChecked;
    }

    public class SubtractCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal SubtractCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.SubtractChecked;
    }
}
