namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class OnesComplementExpressionNode : UnaryExpressionNode
    {
        [DebuggerStepThrough]
        public OnesComplementExpressionNode(INode node) : base(node) { }

        protected override ExpressionType UnaryType => ExpressionType.OnesComplement;
    }
}