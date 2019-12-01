namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class AssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal AssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Assign;
    }
}
