namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class SubtractAssignCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public SubtractAssignCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.SubtractAssignChecked;
    }
}