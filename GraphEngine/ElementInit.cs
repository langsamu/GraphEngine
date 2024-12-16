﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class ElementInit(NodeWithGraph node) : Node(node)
{
    public Method AddMethod
    {
        get => this.GetRequired(ElementInitAddMethod, Method.Parse);

        set => this.SetRequired(ElementInitAddMethod, value);
    }

    public ICollection<Expression> Arguments => this.Collection(ElementInitArguments, Expression.Parse);

    public Linq.ElementInit LinqElementInit => Linq.Expression.ElementInit(this.AddMethod.ReflectionMethod, this.Arguments.LinqExpressions());

    internal static ElementInit Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new ElementInit(node)
    };
}
