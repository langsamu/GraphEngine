namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class SubtractCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal SubtractCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.SubtractChecked;
    }
}
