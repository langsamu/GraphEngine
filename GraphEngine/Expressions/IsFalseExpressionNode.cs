namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class IsFalseExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public IsFalseExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.IsFalse;
    }
}