// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Field(NodeWithGraph node) : MemberAccess(node, Vocabulary.Field)
{
    public override Linq.Expression LinqExpression => this.Type switch
    {
        Type type => Linq.Expression.Field(this.Expression?.LinqExpression, type.SystemType, this.Name),
        _ => Linq.Expression.Field(this.Expression?.LinqExpression, this.Name)
    };
}
