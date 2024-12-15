// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Diagnostics;
using static Vocabulary;
using Linq = System.Linq.Expressions;

public class Throw : Expression
{
    [DebuggerStepThrough]
    public Throw(NodeWithGraph node)
        : base(node)
    {
    }

    public Expression? Value
    {
        get => this.GetOptional(ThrowValue, Expression.Parse);

        set => this.SetOptional(ThrowValue, value);
    }

    public Type? Type
    {
        get => this.GetOptional(ThrowType, Type.Parse);

        set => this.SetOptional(ThrowType, value);
    }

    public override Linq.Expression LinqExpression =>
        Linq.Expression.Throw(
            this.Value?.LinqExpression,
            this.Type?.SystemType ?? typeof(void));
}
