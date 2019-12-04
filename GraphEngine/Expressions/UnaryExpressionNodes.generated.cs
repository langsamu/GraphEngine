namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class ArrayLengthExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal ArrayLengthExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.ArrayLength;
    }

    public class ConvertExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal ConvertExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Convert;
    }

    public class ConvertCheckedExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal ConvertCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.ConvertChecked;
    }

    public class DecrementExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal DecrementExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Decrement;
    }

    public class IncrementExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal IncrementExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Increment;
    }

    public class IsFalseExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal IsFalseExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.IsFalse;
    }

    public class IsTrueExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal IsTrueExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.IsTrue;
    }

    public class NegateExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal NegateExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Negate;
    }

    public class NegateCheckedExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal NegateCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.NegateChecked;
    }

    public class NotExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal NotExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Not;
    }

    public class OnesComplementExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal OnesComplementExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.OnesComplement;
    }

    public class PostDecrementAssignExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal PostDecrementAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.PostDecrementAssign;
    }

    public class PostIncrementAssignExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal PostIncrementAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.PostIncrementAssign;
    }

    public class PreDecrementAssignExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal PreDecrementAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.PreDecrementAssign;
    }

    public class PreIncrementAssignExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal PreIncrementAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.PreIncrementAssign;
    }

    public class QuoteExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal QuoteExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Quote;
    }

    public class ThrowExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal ThrowExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Throw;
    }

    public class TypeAsExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal TypeAsExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.TypeAs;
    }

    public class UnaryPlusExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal UnaryPlusExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.UnaryPlus;
    }

    public class UnboxExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        internal UnboxExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Unbox;
    }
}
