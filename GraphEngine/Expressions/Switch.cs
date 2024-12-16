// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Switch : Expression
{
    [DebuggerStepThrough]
    internal Switch(NodeWithGraph node)
        : base(node)
    {
    }

    public Type? Type
    {
        get => this.GetOptional(SwitchType, Type.Parse);

        set => this.SetOptional(SwitchType, value);
    }

    public Expression SwitchValue
    {
        get => this.GetRequired(SwitchSwitchValue, Expression.Parse);

        set => this.SetRequired(SwitchSwitchValue, value);
    }

    public Expression? DeafultBody
    {
        get => this.GetOptional(SwitchDefaultBody, Expression.Parse);

        set => this.SetOptional(SwitchDefaultBody, value);
    }

    public Method? Comparison
    {
        get => this.GetOptional(SwitchComparison, Method.Parse);

        set => this.SetOptional(SwitchComparison, value);
    }

    public ICollection<Case> Cases => this.Collection(SwitchCases, Case.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.Switch(this.Type?.SystemType, this.SwitchValue.LinqExpression, this.DeafultBody?.LinqExpression, this.Comparison?.ReflectionMethod, this.Cases.Select(@case => @case.LinqSwitchCase));
}
