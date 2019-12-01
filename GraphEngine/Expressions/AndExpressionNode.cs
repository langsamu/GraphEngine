namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class AndExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public AndExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.And;
    }
}