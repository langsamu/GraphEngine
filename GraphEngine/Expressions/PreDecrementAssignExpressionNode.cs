namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class PreDecrementAssignExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public PreDecrementAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.PreDecrementAssign;
    }
}