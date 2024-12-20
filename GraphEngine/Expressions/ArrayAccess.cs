// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class ArrayAccess(NodeWithGraph node) : Expression(node)
{
    public Expression Array
    {
        get => GetRequired(ArrayAccessArray, Expression.Parse);

        set => SetRequired(ArrayAccessArray, value);
    }

    public ICollection<Expression> Indexes => Collection(ArrayAccessIndexes, Expression.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.ArrayAccess(Array.LinqExpression, Indexes.LinqExpressions());
}
