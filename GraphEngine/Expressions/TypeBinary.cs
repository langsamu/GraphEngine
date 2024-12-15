// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System;
using System.Diagnostics;
using static Vocabulary;
using Linq = System.Linq.Expressions;

public class TypeBinary : Expression
{
    [DebuggerStepThrough]
    public TypeBinary(NodeWithGraph node)
        : base(node)
    {
    }

    public ExpressionType ExpressionType
    {
        get => this.GetRequired(TypeBinaryExpressionType, ExpressionType.Parse);

        set => this.SetRequired(TypeBinaryExpressionType, value);
    }

    public Expression Expression
    {
        get => this.GetRequired(TypeBinaryExpression, Expression.Parse);

        set => this.SetRequired(TypeBinaryExpression, value);
    }

    public Type Type
    {
        get => this.GetRequired(TypeBinaryType, Type.Parse);

        set => this.SetRequired(TypeBinaryType, value);
    }

    public override Linq.Expression LinqExpression => this.ExpressionType.LinqExpressionType switch
    {
        Linq.ExpressionType.TypeEqual => Linq.Expression.TypeEqual(this.Expression.LinqExpression, this.Type.SystemType),
        Linq.ExpressionType.TypeIs => Linq.Expression.TypeIs(this.Expression.LinqExpression, this.Type.SystemType),

        var unknown => throw new InvalidOperationException($"{unknown} is not binarytype"),
    };
}
