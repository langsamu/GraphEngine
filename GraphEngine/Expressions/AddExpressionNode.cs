namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class AddExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AddExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Add;
    }
}
