namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class GreaterThanExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public GreaterThanExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.GreaterThan;
    }
}