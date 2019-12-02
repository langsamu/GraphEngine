namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class IsTrueExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public IsTrueExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.IsTrue;
    }
}