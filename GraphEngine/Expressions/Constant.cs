// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Constant(NodeWithGraph node) : Expression(node)
{
    public object? Value
    {
        get => GetOptional(ConstantValue, AsObject);

        set => SetOptional(ConstantValue, value);
    }

    public Type? Type
    {
        get => GetOptional(ConstantType, Type.Parse);

        set => SetOptional(ConstantType, value);
    }

    public override Linq.Expression LinqExpression => Type switch
    {
        Type type => Linq.Expression.Constant(Value, type.SystemType),
        _ => Linq.Expression.Constant(Value)
    };
}
