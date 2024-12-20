// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Case(NodeWithGraph node) : Node(node)
{
    public Expression Body
    {
        get => GetRequired(CaseBody, Expression.Parse);

        set => SetRequired(CaseBody, value);
    }

    public ICollection<Expression> TestValues => Collection(CaseTestValues, Expression.Parse);

    public Linq.SwitchCase LinqSwitchCase => Linq.Expression.SwitchCase(Body.LinqExpression, TestValues.LinqExpressions());

    internal static Case Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new Case(node)
    };
}
