// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class ReferenceNotEqual(NodeWithGraph node) : Binary(node, Vocabulary.ReferenceNotEqual)
{
    public override Linq.Expression LinqExpression => Linq.Expression.ReferenceNotEqual(this.Left.LinqExpression, this.Right.LinqExpression);
}
