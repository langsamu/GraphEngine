namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class ArrayLengthExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public ArrayLengthExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.ArrayLength;
    }
}