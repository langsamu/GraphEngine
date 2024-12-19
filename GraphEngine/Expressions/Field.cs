﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Field(NodeWithGraph node) : MemberAccess(node, Vocabulary.Field)
{
    [DebuggerStepThrough]
    internal Field(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.Field;

    public override Linq.Expression LinqExpression => this.Type switch
    {
        not null => Linq.Expression.Field(this.Expression?.LinqExpression, this.Type.SystemType, this.Name),
        _ => Linq.Expression.Field(this.Expression?.LinqExpression, this.Name)
    };
}
