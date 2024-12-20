// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public abstract class MemberAccess(NodeWithGraph node, INode type) : Expression(node, type)
{
    public Expression? Expression
    {
        get => GetOptional(MemberAccessExpression, Expression.Parse);

        set => SetOptional(MemberAccessExpression, value);
    }

    public string Name
    {
        get => GetRequired(MemberAccessName, AsString);

        set => SetRequired(MemberAccessName, value);
    }

    public Type? Type
    {
        get => GetOptional(MemberAccessType, Type.Parse);

        set => SetOptional(MemberAccessType, value);
    }
}
