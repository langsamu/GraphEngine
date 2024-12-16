// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Constant : Expression
{
    [DebuggerStepThrough]
    internal Constant(NodeWithGraph node)
        : base(node)
    {
    }

    public object? Value
    {
        get => this.GetOptional(ConstantValue, AsObject);

        set => this.SetOptional(ConstantValue, value);
    }

    public Type? Type
    {
        get => this.GetOptional(ConstantType, Type.Parse);

        set => this.SetOptional(ConstantType, value);
    }

    public override Linq.Expression LinqExpression => this.Type switch
    {
        Type type => Linq.Expression.Constant(this.Value, type.SystemType),
        _ => Linq.Expression.Constant(this.Value)
    };
}
