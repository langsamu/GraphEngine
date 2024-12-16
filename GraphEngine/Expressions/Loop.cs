// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

internal class Loop : Expression
{
    [DebuggerStepThrough]
    public Loop(NodeWithGraph node)
        : base(node)
    {
    }

    public Expression Body
    {
        get => this.GetRequired(LoopBody, Expression.Parse);

        set => this.SetRequired(LoopBody, value);
    }

    public Target? Break
    {
        get => this.GetOptional(LoopBreak, Target.Parse);

        set => this.SetOptional(LoopBreak, value);
    }

    public Target? Continue
    {
        get => this.GetOptional(LoopContinue, Target.Parse);

        set => this.SetOptional(LoopContinue, value);
    }

    public override Linq.Expression LinqExpression => this switch
    {
        { Break: not null, Continue: not null } => Linq.Expression.Loop(this.Body.LinqExpression, this.Break.LinqTarget, this.Continue.LinqTarget),
        { Break: not null } => Linq.Expression.Loop(this.Body.LinqExpression, this.Break.LinqTarget),
        _ => Linq.Expression.Loop(this.Body.LinqExpression)
    };
}
