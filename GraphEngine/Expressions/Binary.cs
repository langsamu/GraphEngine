// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Binary(NodeWithGraph node, INode? type = default) : Expression(node, type)
{
    public Expression Left
    {
        get => GetRequired(BinaryLeft, Expression.Parse);

        set => SetRequired(BinaryLeft, value);
    }

    public Expression Right
    {
        get => GetRequired(BinaryRight, Expression.Parse);

        set => SetRequired(BinaryRight, value);
    }

    public ExpressionType ExpressionType
    {
        get => GetRequired(BinaryExpressionType, ExpressionType.Parse);

        set => SetRequired(BinaryExpressionType, value);
    }

    public Method? Method
    {
        get => GetOptional(BinaryMethod, Method.Parse);

        set => SetOptional(BinaryMethod, value);
    }

    public bool? LiftToNull
    {
        get => GetOptionalS(BinaryLiftToNull, AsBool);

        set => SetOptional(BinaryLiftToNull, value);
    }

    public Lambda? Conversion
    {
        get => GetOptional(BinaryConversion, Lambda.Parse);

        set => SetOptional(BinaryConversion, value);
    }

    public override Linq.Expression LinqExpression =>
        Linq.Expression.MakeBinary(
            ExpressionType.LinqExpressionType,
            Left.LinqExpression,
            Right.LinqExpression,
            LiftToNull ?? false,
            Method?.ReflectionMethod,
            Conversion?.LinqLambda);

    public static Binary Create(NodeWithGraph node, Linq.ExpressionType type) => (node, type) switch
    {
        (node: NodeWithGraph n, type:
            Linq.ExpressionType.Add or
            Linq.ExpressionType.AddAssign or
            Linq.ExpressionType.AddAssignChecked or
            Linq.ExpressionType.AddChecked or
            Linq.ExpressionType.And or
            Linq.ExpressionType.AndAlso or
            Linq.ExpressionType.AndAssign or
            Linq.ExpressionType.ArrayIndex or
            Linq.ExpressionType.Assign or
            Linq.ExpressionType.Coalesce or
            Linq.ExpressionType.Divide or
            Linq.ExpressionType.DivideAssign or
            Linq.ExpressionType.Equal or
            Linq.ExpressionType.ExclusiveOr or
            Linq.ExpressionType.ExclusiveOrAssign or
            Linq.ExpressionType.GreaterThan or
            Linq.ExpressionType.GreaterThanOrEqual or
            Linq.ExpressionType.LeftShift or
            Linq.ExpressionType.LeftShiftAssign or
            Linq.ExpressionType.LessThan or
            Linq.ExpressionType.LessThanOrEqual or
            Linq.ExpressionType.Modulo or
            Linq.ExpressionType.ModuloAssign or
            Linq.ExpressionType.Multiply or
            Linq.ExpressionType.MultiplyAssign or
            Linq.ExpressionType.MultiplyAssignChecked or
            Linq.ExpressionType.MultiplyChecked or
            Linq.ExpressionType.NotEqual or
            Linq.ExpressionType.Or or
            Linq.ExpressionType.OrAssign or
            Linq.ExpressionType.OrElse or
            Linq.ExpressionType.Power or
            Linq.ExpressionType.PowerAssign or
            Linq.ExpressionType.RightShift or
            Linq.ExpressionType.RightShiftAssign or
            Linq.ExpressionType.Subtract or
            Linq.ExpressionType.SubtractAssign or
            Linq.ExpressionType.SubtractAssignChecked or
            Linq.ExpressionType.SubtractChecked) => new Binary(n)
            {
                ExpressionType = ExpressionType.Create(type, n.Graph),
            },

        (node: null, _) => throw new ArgumentNullException(nameof(node)),
        _ => throw new InvalidOperationException($"{type} is not binary"),
    };
}
