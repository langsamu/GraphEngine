namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class ThrowExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public ThrowExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Throw;
    }
}