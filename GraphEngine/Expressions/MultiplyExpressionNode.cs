namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class MultiplyExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public MultiplyExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Multiply;
    }
}