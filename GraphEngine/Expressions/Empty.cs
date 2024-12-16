// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Empty : Expression
{
    [DebuggerStepThrough]
    internal Empty(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.Empty;

    public override Linq.Expression LinqExpression => Linq.Expression.Empty();
}
