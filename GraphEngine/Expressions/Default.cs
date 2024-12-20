// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Default(NodeWithGraph node) : Expression(node)
{
    public Type Type
    {
        get => GetRequired(DefaultType, Type.Parse);

        set => SetRequired(DefaultType, value);
    }

    public override Linq.Expression LinqExpression => Linq.Expression.Default(Type.SystemType);
}
