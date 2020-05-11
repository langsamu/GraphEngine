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

        public static Goto Create(INode node) => new Goto(node) { RdfType = Vocabulary.Goto };
    }

    public class Return : BaseGoto
    {
        [DebuggerStepThrough]
        internal Return(INode node) : base(node) { }

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Return;

        public static Return Create(INode node) => new Return(node) { RdfType = Vocabulary.Return };
    }

    public class Break : BaseGoto
    {
        [DebuggerStepThrough]
        internal Break(INode node) : base(node) { }

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Break;

        public static Break Create(INode node) => new Break(node) { RdfType = Vocabulary.Break };
    }

    public class Continue : BaseGoto
    {
        [DebuggerStepThrough]
        internal Continue(INode node) : base(node) { }

        protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Continue;

        public static Continue Create(INode node) => new Continue(node) { RdfType = Vocabulary.Continue };
    }
}
