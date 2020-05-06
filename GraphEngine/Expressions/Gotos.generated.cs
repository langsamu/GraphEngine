// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Goto : BaseGoto
    {
        [DebuggerStepThrough]
        internal Goto(INode node) : base(node) { }

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Goto;
    }

    public class Return : BaseGoto
    {
        [DebuggerStepThrough]
        internal Return(INode node) : base(node) { }

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Return;
    }

    public class Break : BaseGoto
    {
        [DebuggerStepThrough]
        internal Break(INode node) : base(node) { }

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Break;
    }

    public class Continue : BaseGoto
    {
        [DebuggerStepThrough]
        internal Continue(INode node) : base(node) { }

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Continue;
    }
}
