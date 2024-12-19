// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Throw(NodeWithGraph node, INode? type = default) : Expression(node, type)
{
    public Expression? Value
    {
        get => this.GetOptional(ThrowValue, Expression.Parse);

        set => this.SetOptional(ThrowValue, value);
    }

    public Type? Type
    {
        get => this.GetOptional(ThrowType, Type.Parse);

        set => this.SetOptional(ThrowType, value);
    }

    public override Linq.Expression LinqExpression =>
        Linq.Expression.Throw(
            this.Value?.LinqExpression,
            this.Type?.SystemType ?? typeof(void));
}
