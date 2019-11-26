namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class AssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AssignExpressionNode(INode node) : base(node) { }

        public override Expression Expression => Expression.Assign(Left.Expression, Right.Expression);
    }
}
