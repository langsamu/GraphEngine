namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class RightShiftAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public RightShiftAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.RightShiftAssign;
    }
}