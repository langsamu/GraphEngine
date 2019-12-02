namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class NotExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public NotExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Not;
    }
}