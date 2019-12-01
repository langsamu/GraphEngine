using VDS.RDF;
namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class LessThanOrEqualExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public LessThanOrEqualExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.LessThanOrEqual;
    }
}