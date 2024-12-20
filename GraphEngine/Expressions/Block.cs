// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Block(NodeWithGraph node) : Expression(node)
{
    public Type? Type
    {
        get => GetOptional(BlockType, Type.Parse);

        set => SetOptional(BlockType, value);
    }

    public ICollection<Expression> Expressions => Collection(BlockExpressions, Expression.Parse);

    public ICollection<Parameter> Variables => Collection(BlockVariables, Parameter.Parse);

    public override Linq.Expression LinqExpression => (type: Type, empty: !Variables.Any()) switch
    {
        (type: not null, _) => Linq.Expression.Block(
            Type.SystemType,
            from e in Variables select e.LinqParameter,
            Expressions.LinqExpressions()),

        (_, empty: true) => Linq.Expression.Block(
            Expressions.LinqExpressions().ToArray()),

        _ => Linq.Expression.Block(
            from e in Variables select e.LinqParameter,
            Expressions.LinqExpressions()),
    };
}
