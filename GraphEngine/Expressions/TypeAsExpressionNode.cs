namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class TypeAsExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public TypeAsExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.TypeAs;
    }
}