namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class CoalesceExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public CoalesceExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Coalesce;
    }
}