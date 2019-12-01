namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class MultiplyAssignCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public MultiplyAssignCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.MultiplyAssignChecked;
    }
}