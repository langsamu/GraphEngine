// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public abstract class BaseGoto(NodeWithGraph node, INode type) : Expression(node, type)
{
    public Target Target
    {
        get => GetRequired(GotoTarget, Target.Parse);

        set => SetRequired(GotoTarget, value);
    }

    public Type? Type
    {
        get => GetOptional(GotoType, Type.Parse);

        set => SetOptional(GotoType, value);
    }

    public Expression? Value
    {
        get => GetOptional(GotoValue, Expression.Parse);

        set => SetOptional(GotoValue, value);
    }

    public override Linq.Expression LinqExpression => Linq.Expression.MakeGoto(Kind, Target.LinqTarget, Value?.LinqExpression, Type?.SystemType ?? typeof(void));

    protected abstract Linq.GotoExpressionKind Kind { get; }

    public static BaseGoto Create(NodeWithGraph node, Linq.GotoExpressionKind kind) => kind switch
    {
        Linq.GotoExpressionKind.Goto => new Goto(node),
        Linq.GotoExpressionKind.Return => new Return(node),
        Linq.GotoExpressionKind.Break => new Break(node),
        Linq.GotoExpressionKind.Continue => new Continue(node),

        _ => throw new GraphEngineException("{type} is not goto")
    };
}
