namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class QuoteExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public QuoteExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Quote;
    }
}