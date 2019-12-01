namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class MultiplyAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public MultiplyAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.MultiplyAssign;
    }
}