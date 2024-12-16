﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Block(NodeWithGraph node) : Expression(node)
{
    public Type? Type
    {
        get => this.GetOptional(BlockType, Type.Parse);

        set => this.SetOptional(BlockType, value);
    }

    public ICollection<Expression> Expressions => this.Collection(BlockExpressions, Expression.Parse);

    public ICollection<Parameter> Variables => this.Collection(BlockVariables, Parameter.Parse);

    public override Linq.Expression LinqExpression => (type: this.Type, empty: !this.Variables.Any()) switch
    {
        (type: not null, _) => Linq.Expression.Block(
            this.Type.SystemType,
            from e in this.Variables select e.LinqParameter,
            this.Expressions.LinqExpressions()),

        (_, empty: true) => Linq.Expression.Block(
            this.Expressions.LinqExpressions().ToArray()),

        _ => Linq.Expression.Block(
            from e in this.Variables select e.LinqParameter,
            this.Expressions.LinqExpressions()),
    };
}
