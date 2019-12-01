namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class PowerAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public PowerAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.PowerAssign;
    }
}