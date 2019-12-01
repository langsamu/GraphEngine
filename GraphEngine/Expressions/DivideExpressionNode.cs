namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class DivideExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public DivideExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Divide;
    }
}