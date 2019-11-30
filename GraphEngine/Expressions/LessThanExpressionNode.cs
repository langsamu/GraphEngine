namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class LessThanExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal LessThanExpressionNode(INode node) : base(node) { }

        protected override ExpressionType Type => ExpressionType.LessThan;
    }
}
