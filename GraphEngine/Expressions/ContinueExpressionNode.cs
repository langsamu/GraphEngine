namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class ContinueExpressionNode : GotoExpressionNode
    {
        [DebuggerStepThrough]
        public ContinueExpressionNode(INode node) : base(node) { }

        protected override GotoExpressionKind Kind => GotoExpressionKind.Continue;
    }
}
