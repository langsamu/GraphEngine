namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class OrAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public OrAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.OrAssign;
    }
}