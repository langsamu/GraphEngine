// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Condition(NodeWithGraph node, INode? type = default) : Expression(node, type)
{
    public Expression Test
    {
        get => GetRequired(ConditionTest, Expression.Parse);

        set => SetRequired(ConditionTest, value);
    }

    public Expression IfTrue
    {
        get => GetRequired(ConditionIfTrue, Expression.Parse);

        set => SetRequired(ConditionIfTrue, value);
    }

    public Expression IfFalse
    {
        get => GetRequired(ConditionIfFalse, Expression.Parse);

        set => SetRequired(ConditionIfFalse, value);
    }

    public Type? Type
    {
        get => GetOptional(ConditionType, Type.Parse);

        set => SetOptional(ConditionType, value);
    }

    public override Linq.Expression LinqExpression => Type switch
    {
        Type type => Linq.Expression.Condition(Test.LinqExpression, IfTrue.LinqExpression, IfFalse.LinqExpression, type.SystemType),
        _ => Linq.Expression.Condition(Test.LinqExpression, IfTrue.LinqExpression, IfFalse.LinqExpression)
    };
}
