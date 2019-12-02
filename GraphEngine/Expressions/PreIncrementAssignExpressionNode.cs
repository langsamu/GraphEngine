namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class PreIncrementAssignExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public PreIncrementAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.PreIncrementAssign;
    }
}