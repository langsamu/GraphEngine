namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class UnaryPlusExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public UnaryPlusExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.UnaryPlus;
    }
}