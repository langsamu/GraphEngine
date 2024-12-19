// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class NewArrayBounds(NodeWithGraph node) : NewArray(node, Vocabulary.NewArrayBounds)
{
    protected override NewArrayExpressionFactory FactoryMethod => Linq.Expression.NewArrayBounds;
}
