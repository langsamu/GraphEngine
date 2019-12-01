namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class ExclusiveOrAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public ExclusiveOrAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.ExclusiveOrAssign;
    }
}