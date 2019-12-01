namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class AndAlsoExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public AndAlsoExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AndAlso;
    }
}