// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Switch(NodeWithGraph node) : Expression(node)
{
    public Type? Type
    {
        get => GetOptional(SwitchType, Type.Parse);

        set => SetOptional(SwitchType, value);
    }

    public Expression SwitchValue
    {
        get => GetRequired(SwitchSwitchValue, Expression.Parse);

        set => SetRequired(SwitchSwitchValue, value);
    }

    public Expression? DeafultBody
    {
        get => GetOptional(SwitchDefaultBody, Expression.Parse);

        set => SetOptional(SwitchDefaultBody, value);
    }

    public Method? Comparison
    {
        get => GetOptional(SwitchComparison, Method.Parse);

        set => SetOptional(SwitchComparison, value);
    }

    public ICollection<Case> Cases => Collection(SwitchCases, Case.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.Switch(Type?.SystemType, SwitchValue.LinqExpression, DeafultBody?.LinqExpression, Comparison?.ReflectionMethod, Cases.Select(@case => @case.LinqSwitchCase));
}
