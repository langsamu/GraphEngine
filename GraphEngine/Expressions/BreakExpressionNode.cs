namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class BreakExpressionNode : GotoExpressionNode
    {
        [DebuggerStepThrough]
        public BreakExpressionNode(INode node) : base(node) { }

        protected override GotoExpressionKind Kind => GotoExpressionKind.Break;
    }
}
