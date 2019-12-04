// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class GotoExpressionNode : BaseGotoExpressionNode
    {
        [DebuggerStepThrough]
        internal GotoExpressionNode(INode node) : base(node) { }

        protected override GotoExpressionKind Kind => GotoExpressionKind.Goto;
    }

    public class ReturnExpressionNode : BaseGotoExpressionNode
    {
        [DebuggerStepThrough]
        internal ReturnExpressionNode(INode node) : base(node) { }

        protected override GotoExpressionKind Kind => GotoExpressionKind.Return;
    }

    public class BreakExpressionNode : BaseGotoExpressionNode
    {
        [DebuggerStepThrough]
        internal BreakExpressionNode(INode node) : base(node) { }

        protected override GotoExpressionKind Kind => GotoExpressionKind.Break;
    }

    public class ContinueExpressionNode : BaseGotoExpressionNode
    {
        [DebuggerStepThrough]
        internal ContinueExpressionNode(INode node) : base(node) { }

        protected override GotoExpressionKind Kind => GotoExpressionKind.Continue;
    }
}
