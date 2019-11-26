namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class SubtractExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal SubtractExpressionNode(INode node) : base(node) { }

        public override Expression Expression => Expression.Subtract(this.Left.Expression, this.Right.Expression);
    }
}
