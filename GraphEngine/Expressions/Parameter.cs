﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Parameter(NodeWithGraph node) : Expression(node)
{
    private static readonly IDictionary<INode, Linq.ParameterExpression> Cache = new Dictionary<INode, Linq.ParameterExpression>();

    public Type Type
    {
        get => this.GetRequired(ParameterType, Type.Parse);

        set => this.SetRequired(ParameterType, value);
    }

    public string? Name
    {
        get => this.GetOptional(ParameterName, AsString);

        set => this.SetOptional(ParameterName, value);
    }

    public override Linq.Expression LinqExpression => this.LinqParameter;

    public Linq.ParameterExpression LinqParameter
    {
        get
        {
            if (!Cache.TryGetValue(this, out var param))
            {
                param = Cache[this] = Linq.Expression.Parameter(this.Type.SystemType, this.Name);
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
