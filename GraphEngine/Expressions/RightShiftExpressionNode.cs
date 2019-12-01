namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class RightShiftExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public RightShiftExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.RightShift;
    }
}