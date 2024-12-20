// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class ArrayIndex(NodeWithGraph node) : Expression(node)
{
    public Expression Array
    {
        get => GetRequired(ArrayIndexArray, Expression.Parse);

        set => SetRequired(ArrayIndexArray, value);
    }

    public Expression? Index
    {
        get => GetOptional(ArrayIndexIndex, Expression.Parse);

        set => SetOptional(ArrayIndexIndex, value);
    }

    public ICollection<Expression> Indexes => Collection(ArrayIndexIndexes, Expression.Parse);

    public override Linq.Expression LinqExpression => Index switch
    {
        not null => Linq.Expression.ArrayIndex(Array.LinqExpression, Index.LinqExpression),
        _ => Linq.Expression.ArrayIndex(Array.LinqExpression, Indexes.LinqExpressions())
    };
}
