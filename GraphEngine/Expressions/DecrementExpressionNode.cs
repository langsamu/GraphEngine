namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class DecrementExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public DecrementExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Decrement;
    }
}