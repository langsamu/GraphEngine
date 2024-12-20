// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class PropertyOrField(NodeWithGraph node) : MemberAccess(node, Vocabulary.PropertyOrField)
{
    public override Linq.Expression LinqExpression => Expression switch
    {
        not null => Linq.Expression.PropertyOrField(Expression.LinqExpression, Name),
        _ => throw new InvalidOperationException($"Expression missing from node {this}")
    };
}
