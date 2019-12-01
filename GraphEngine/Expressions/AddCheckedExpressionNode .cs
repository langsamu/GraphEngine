namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class AddCheckedExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AddCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AddChecked;
    }
}
