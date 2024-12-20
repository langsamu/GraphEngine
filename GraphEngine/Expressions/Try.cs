// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Try(NodeWithGraph node) : Expression(node)
{
    public Type? Type
    {
        get => GetOptional(TryType, Type.Parse);

        set => SetOptional(TryType, value);
    }

    public Expression Body
    {
        get => GetRequired(TryBody, Expression.Parse);

        set => SetRequired(TryBody, value);
    }

    public Expression? Finally
    {
        get => GetOptional(TryFinally, Expression.Parse);

        set => SetOptional(TryFinally, value);
    }

    public Expression? Fault
    {
        get => GetOptional(TryFault, Expression.Parse);

        set => SetOptional(TryFault, value);
    }

    public ICollection<Catch> Handlers => Collection(TryHandlers, Catch.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.MakeTry(
        Type?.SystemType,
        Body.LinqExpression,
        Finally?.LinqExpression,
        Fault?.LinqExpression,
        from h in Handlers select h.LinqCatchBlock);
}
