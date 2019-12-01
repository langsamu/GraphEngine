namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class OrExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public OrExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Or;
    }
}