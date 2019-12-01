namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class SubtractAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public SubtractAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.SubtractAssign;
    }
}