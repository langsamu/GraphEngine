namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class MultiplyCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public MultiplyCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.MultiplyChecked;
    }
}