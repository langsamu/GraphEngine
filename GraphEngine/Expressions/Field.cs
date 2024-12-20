// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Field(NodeWithGraph node) : MemberAccess(node, Vocabulary.Field)
{
    public override Linq.Expression LinqExpression => Type switch
    {
        Type type => Linq.Expression.Field(Expression?.LinqExpression, type.SystemType, Name),
        _ => Linq.Expression.Field(Expression?.LinqExpression, Name)
    };
}
