namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class AndAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public AndAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AndAssign;
    }
}