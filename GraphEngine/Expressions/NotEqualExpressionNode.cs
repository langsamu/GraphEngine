namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class NotEqualExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public NotEqualExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.NotEqual;
    }
}