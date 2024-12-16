// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Label : Expression
{
    [DebuggerStepThrough]
    internal Label(NodeWithGraph node)
        : base(node)
    {
    }

    public Target Target
    {
        get => this.GetRequired(LabelTarget, Target.Parse);

        set => this.SetRequired(LabelTarget, value);
    }

    public Expression? DefaultValue
    {
        get => this.GetOptional(LabelDefaultValue, Expression.Parse);

        set => this.SetOptional(LabelDefaultValue, value);
    }

    public override Linq.Expression LinqExpression => Linq.Expression.Label(this.Target.LinqTarget, this.DefaultValue?.LinqExpression);
}
