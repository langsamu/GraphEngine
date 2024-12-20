// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class ReferenceEqual(NodeWithGraph node) : Binary(node, Vocabulary.ReferenceEqual)
{
    public override Linq.Expression LinqExpression => Linq.Expression.ReferenceEqual(Left.LinqExpression, Right.LinqExpression);
}
