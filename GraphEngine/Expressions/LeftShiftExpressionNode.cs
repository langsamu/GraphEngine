namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class LeftShiftExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public LeftShiftExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.LeftShift;
    }
}