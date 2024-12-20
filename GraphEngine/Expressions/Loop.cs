// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

internal class Loop(NodeWithGraph node) : Expression(node)
{
    public Expression Body
    {
        get => GetRequired(LoopBody, Expression.Parse);

        set => SetRequired(LoopBody, value);
    }

    public Target? Break
    {
        get => GetOptional(LoopBreak, Target.Parse);

        set => SetOptional(LoopBreak, value);
    }

    public Target? Continue
    {
        get => GetOptional(LoopContinue, Target.Parse);

        set => SetOptional(LoopContinue, value);
    }

    public override Linq.Expression LinqExpression => this switch
    {
        { Break: not null, Continue: not null } => Linq.Expression.Loop(Body.LinqExpression, Break.LinqTarget, Continue.LinqTarget),
        { Break: not null } => Linq.Expression.Loop(Body.LinqExpression, Break.LinqTarget),
        _ => Linq.Expression.Loop(Body.LinqExpression)
    };
}
