// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class TypeBinary(NodeWithGraph node) : Expression(node)
{
    public ExpressionType ExpressionType
    {
        get => GetRequired(TypeBinaryExpressionType, ExpressionType.Parse);

        set => SetRequired(TypeBinaryExpressionType, value);
    }

    public Expression Expression
    {
        get => GetRequired(TypeBinaryExpression, Expression.Parse);

        set => SetRequired(TypeBinaryExpression, value);
    }

    public Type Type
    {
        get => GetRequired(TypeBinaryType, Type.Parse);

        set => SetRequired(TypeBinaryType, value);
    }

    public override Linq.Expression LinqExpression => ExpressionType.LinqExpressionType switch
    {
        Linq.ExpressionType.TypeEqual => Linq.Expression.TypeEqual(Expression.LinqExpression, Type.SystemType),
        Linq.ExpressionType.TypeIs => Linq.Expression.TypeIs(Expression.LinqExpression, Type.SystemType),

        var unknown => throw new InvalidOperationException($"{unknown} is not binarytype"),
    };
}
