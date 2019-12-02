namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class NegateCheckedExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public NegateCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.NegateChecked;
    }
}