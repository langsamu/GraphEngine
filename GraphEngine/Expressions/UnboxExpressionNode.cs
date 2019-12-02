namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class UnboxExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public UnboxExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Unbox;
    }
}