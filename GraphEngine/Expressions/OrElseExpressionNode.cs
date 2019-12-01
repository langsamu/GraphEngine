namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class OrElseExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public OrElseExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.OrElse;
    }
}