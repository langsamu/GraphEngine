// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Dynamic(NodeWithGraph node) : Expression(node)
{
    public Binder Binder
    {
        get => GetRequired(DynamicBinder, Binder.Parse);

        set => SetRequired(DynamicBinder, value);
    }

    public Type ReturnType
    {
        get => GetRequired(DynamicReturnType, Type.Parse);

        set => SetRequired(DynamicReturnType, value);
    }

    public ICollection<Expression> Arguments => Collection(DynamicArguments, Expression.Parse);

    public override Linq.Expression LinqExpression =>
        Linq.Expression.Dynamic(
            Binder.SystemBinder,
            ReturnType.SystemType,
            Arguments.LinqExpressions());
}
