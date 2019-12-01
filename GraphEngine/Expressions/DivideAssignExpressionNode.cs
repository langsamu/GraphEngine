namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class DivideAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public DivideAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.DivideAssign;
    }
}