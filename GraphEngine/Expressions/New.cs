// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VDS.RDF;
using static Vocabulary;
using Linq = System.Linq.Expressions;

// TODO: Add overloads
// TODO: Create ConstructorInfo node
public class New : Expression
{
    [DebuggerStepThrough]
    internal New(NodeWithGraph node)
        : base(node)
    {
    }

    public ICollection<Expression> Arguments => this.Collection(NewArguments, Expression.Parse);

    public Type Type
    {
        get => this.GetRequired(NewType, Type.Parse);

        set => this.SetRequired(NewType, value);
    }

    public override Linq.Expression LinqExpression => this.LinqNewExpression;

    internal Linq.NewExpression LinqNewExpression
    {
        get
        {
            var argumentExpressions = this.Arguments.LinqExpressions();
            var types = (from expression in argumentExpressions select expression.Type).ToArray();
            var constructor = this.Type.SystemType.GetConstructor(types);

            return Linq.Expression.New(constructor, argumentExpressions);
        }
    }

    internal static new New Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new New(node)
    };
}
