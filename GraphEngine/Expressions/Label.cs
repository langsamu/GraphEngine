// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Label(NodeWithGraph node) : Expression(node)
{
    public Target Target
    {
        get => GetRequired(LabelTarget, Target.Parse);

        set => SetRequired(LabelTarget, value);
    }

    public Expression? DefaultValue
    {
        get => GetOptional(LabelDefaultValue, Expression.Parse);

        set => SetOptional(LabelDefaultValue, value);
    }

    public override Linq.Expression LinqExpression => Linq.Expression.Label(Target.LinqTarget, DefaultValue?.LinqExpression);
}
