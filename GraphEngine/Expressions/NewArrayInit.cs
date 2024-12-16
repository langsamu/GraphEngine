// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class NewArrayInit : NewArray
{
    [DebuggerStepThrough]
    internal NewArrayInit(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.NewArrayInit;

    protected override NewArrayExpressionFactory FactoryMethod => Linq.Expression.NewArrayInit;
}
