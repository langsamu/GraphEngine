namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class ExclusiveOrExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public ExclusiveOrExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.ExclusiveOr;
    }
}