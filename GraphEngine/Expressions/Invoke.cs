// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Invoke(NodeWithGraph node) : Expression(node)
{
    public Expression Expression
    {
        get => GetRequired(InvokeExpression, Expression.Parse);

        set => SetRequired(InvokeExpression, value);
    }

    public ICollection<Expression> Arguments => Collection(InvokeArguments, Expression.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.Invoke(Expression.LinqExpression, Arguments.LinqExpressions());
}
