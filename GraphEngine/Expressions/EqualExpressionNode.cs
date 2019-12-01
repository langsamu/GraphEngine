namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class EqualExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public EqualExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Equal;
    }
}