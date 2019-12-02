namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class ReturnExpressionNode : GotoExpressionNode
    {
        [DebuggerStepThrough]
        public ReturnExpressionNode(INode node) : base(node) { }

        protected override GotoExpressionKind Kind => GotoExpressionKind.Return;
    }
}
