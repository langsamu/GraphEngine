namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class IncrementExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public IncrementExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Increment;
    }
}