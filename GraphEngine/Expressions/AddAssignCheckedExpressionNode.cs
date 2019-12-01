namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class AddAssignCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public AddAssignCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AddAssignChecked;
    }
}