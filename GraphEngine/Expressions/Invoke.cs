// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Invoke(NodeWithGraph node) : Expression(node)
{
    public Expression Expression
    {
        get => this.GetRequired(InvokeExpression, Expression.Parse);

        set => this.SetRequired(InvokeExpression, value);
    }

    public ICollection<Expression> Arguments => this.Collection(InvokeArguments, Expression.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.Invoke(this.Expression.LinqExpression, this.Arguments.LinqExpressions());
}
