// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using Linq = System.Linq.Expressions;

    public class Goto : BaseGoto
    {
        [DebuggerStepThrough]
        internal Goto(NodeWithGraph node) : base(node) => this.RdfType = Vocabulary.Goto;

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Goto;
    }

    public class Return : BaseGoto
    {
        [DebuggerStepThrough]
        internal Return(NodeWithGraph node) : base(node) => this.RdfType = Vocabulary.Return;

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Return;
    }

    public class Break : BaseGoto
    {
        [DebuggerStepThrough]
        internal Break(NodeWithGraph node) : base(node) => this.RdfType = Vocabulary.Break;

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Break;
    }

    public class Continue : BaseGoto
    {
        [DebuggerStepThrough]
        internal Continue(NodeWithGraph node) : base(node) => this.RdfType = Vocabulary.Continue;

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Continue;
    }
}
