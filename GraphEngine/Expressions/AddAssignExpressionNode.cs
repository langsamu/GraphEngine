namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class AddAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public AddAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.AddAssign;
    }
}