namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class GreaterThanOrEqualExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public GreaterThanOrEqualExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.GreaterThanOrEqual;
    }
}