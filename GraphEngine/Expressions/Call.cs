// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Call(NodeWithGraph node) : Expression(node)
{
    public Expression? Instance
    {
        get => GetOptional(CallInstance, Expression.Parse);

        set => SetOptional(CallInstance, value);
    }

    public Type? Type
    {
        get => GetOptional(CallType, Type.Parse);

        set => SetOptional(CallType, value);
    }

    public Method? Method
    {
        get => GetOptional(CallMethod, Method.Parse);

        set => SetOptional(CallMethod, value);
    }

    public string? MethodName
    {
        get => GetOptional(CallMethodName, AsString);

        set => SetOptional(CallMethodName, value);
    }

    public ICollection<Expression> Arguments => Collection(CallArguments, Expression.Parse);

    public ICollection<Type> TypeArguments => Collection(CallTypeArguments, Type.Parse);

    public override Linq.Expression LinqExpression => this switch
    {
        Call { Method.ReflectionMethod: not null } => Linq.Expression.Call(
           Instance?.LinqExpression,
           Method.ReflectionMethod,
           Arguments.LinqExpressions()),

        Call { Type: not null } => Linq.Expression.Call(
            Type.SystemType,
            MethodName,
            (from typeArg in TypeArguments select typeArg.SystemType).ToArray(),
            Arguments.LinqExpressions().ToArray()),

        _ => Linq.Expression.Call(
            Instance.LinqExpression,
            MethodName,
            (from typeArg in TypeArguments select typeArg.SystemType).ToArray(),
            Arguments.LinqExpressions().ToArray())
    };
}
