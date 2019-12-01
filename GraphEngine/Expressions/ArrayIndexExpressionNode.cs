namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class ArrayIndexExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public ArrayIndexExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.ArrayIndex;
    }
}