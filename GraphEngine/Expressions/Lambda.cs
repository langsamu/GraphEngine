// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Lambda(NodeWithGraph node) : Expression(node)
{
    public Expression Body
    {
        get => GetRequired(LambdaBody, Expression.Parse);

        set => SetRequired(LambdaBody, value);
    }

    public ICollection<Parameter> Parameters => Collection(LambdaParameters, Parameter.Parse);

    public override Linq.Expression LinqExpression => LinqLambda;

    public Linq.LambdaExpression LinqLambda =>
        Linq.Expression.Lambda(
            Body.LinqExpression,
            from param in Parameters select param.LinqParameter);

    internal static new Lambda Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new Lambda(node)
    };
}
