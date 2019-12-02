namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class NegateExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public NegateExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Negate;
    }
}