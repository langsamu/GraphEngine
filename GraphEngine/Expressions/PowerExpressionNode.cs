namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class PowerExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public PowerExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Power;
    }
}