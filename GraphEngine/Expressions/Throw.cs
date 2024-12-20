// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Throw(NodeWithGraph node, INode? type = default) : Expression(node, type)
{
    public Expression? Value
    {
        get => GetOptional(ThrowValue, Expression.Parse);

        set => SetOptional(ThrowValue, value);
    }

    public Type? Type
    {
        get => GetOptional(ThrowType, Type.Parse);

        set => SetOptional(ThrowType, value);
    }

    public override Linq.Expression LinqExpression =>
        Linq.Expression.Throw(
            Value?.LinqExpression,
            Type?.SystemType ?? typeof(void));
}
