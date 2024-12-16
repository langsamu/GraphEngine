// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class NewArrayBounds : NewArray
{
    [DebuggerStepThrough]
    internal NewArrayBounds(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.NewArrayBounds;

    protected override NewArrayExpressionFactory FactoryMethod => Linq.Expression.NewArrayBounds;
}
