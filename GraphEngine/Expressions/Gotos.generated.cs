// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Goto(NodeWithGraph node) : BaseGoto(node, Vocabulary.Goto)
{
    protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Goto;
}

public class Return(NodeWithGraph node) : BaseGoto(node, Vocabulary.Return)
{
    protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Return;
}

public class Break(NodeWithGraph node) : BaseGoto(node, Vocabulary.Break)
{
    protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Break;
}

public class Continue(NodeWithGraph node) : BaseGoto(node, Vocabulary.Continue)
{
    protected override Linq.GotoExpressionKind Kind => Linq.GotoExpressionKind.Continue;
}

