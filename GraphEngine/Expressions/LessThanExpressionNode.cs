namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class LessThanExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal LessThanExpressionNode(INode node) : base(node) { }

        public override Expression Expression => Expression.LessThan(Left.Expression, Right.Expression);
    }
}
