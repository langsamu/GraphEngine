namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class ConvertExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public ConvertExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.Convert;
    }
}