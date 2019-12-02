namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class PostDecrementAssignExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public PostDecrementAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.PostDecrementAssign;
    }
}