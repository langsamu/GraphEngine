namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class ConvertCheckedExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public ConvertCheckedExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.ConvertChecked;
    }
}