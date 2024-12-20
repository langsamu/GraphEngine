// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Parameter(NodeWithGraph node) : Expression(node)
{
    private static readonly IDictionary<INode, Linq.ParameterExpression> Cache = new Dictionary<INode, Linq.ParameterExpression>();

    public Type Type
    {
        get => GetRequired(ParameterType, Type.Parse);

        set => SetRequired(ParameterType, value);
    }

    public string? Name
    {
        get => GetOptional(ParameterName, AsString);

        set => SetOptional(ParameterName, value);
    }

    public override Linq.Expression LinqExpression => LinqParameter;

    public Linq.ParameterExpression LinqParameter
    {
        get
        {
            if (!Cache.TryGetValue(this, out var param))
            {
                param = Cache[this] = Linq.Expression.Parameter(Type.SystemType, Name);
            }

            return param;
        }
    }

    internal static new Parameter Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new Parameter(node)
    };
}
