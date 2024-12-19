// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class NewArrayInit(NodeWithGraph node) : NewArray(node, Vocabulary.NewArrayInit)
{
    protected override NewArrayExpressionFactory FactoryMethod => Linq.Expression.NewArrayInit;
}
