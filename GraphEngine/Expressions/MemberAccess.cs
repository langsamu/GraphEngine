// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public abstract class MemberAccess : Expression
{
    [DebuggerStepThrough]
    internal MemberAccess(NodeWithGraph node)
        : base(node)
    {
    }

    public Expression? Expression
    {
        get => this.GetOptional(MemberAccessExpression, Expression.Parse);

        set => this.SetOptional(MemberAccessExpression, value);
    }

    public string Name
    {
        get => this.GetRequired(MemberAccessName, AsString);

        set => this.SetRequired(MemberAccessName, value);
    }

    public Type? Type
    {
        get => this.GetOptional(MemberAccessType, Type.Parse);

        set => this.SetOptional(MemberAccessType, value);
    }
}
