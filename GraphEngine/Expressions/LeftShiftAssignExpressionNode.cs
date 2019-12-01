namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class LeftShiftAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public LeftShiftAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.LeftShiftAssign;
    }
}