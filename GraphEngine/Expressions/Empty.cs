// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Empty(NodeWithGraph node) : Expression(node, Vocabulary.Empty)
{
    public override Linq.Expression LinqExpression => Linq.Expression.Empty();
}
