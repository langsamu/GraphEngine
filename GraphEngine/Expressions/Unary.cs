// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Unary(NodeWithGraph node) : Expression(node)
{
    public Expression Operand
    {
        get => this.GetRequired(UnaryOperand, Expression.Parse);

        set => this.SetRequired(UnaryOperand, value);
    }

    public Type? Type
    {
        get => this.GetOptional(UnaryType, Type.Parse);

        set => this.SetOptional(UnaryType, value);
    }

    public ExpressionType ExpressionType
    {
        get => this.GetRequired(UnaryExpressionType, ExpressionType.Parse);

        set => this.SetRequired(UnaryExpressionType, value);
    }

    public Method? Method
    {
        get => this.GetOptional(UnaryMethod, Method.Parse);

        set => this.SetOptional(UnaryMethod, value);
    }

    public override Linq.Expression LinqExpression => Linq.Expression.MakeUnary(
        this.ExpressionType.LinqExpressionType,
        this.Operand.LinqExpression,
        this.Type?.SystemType,
        this.Method?.ReflectionMethod);

    public static Unary Create(NodeWithGraph node, Linq.ExpressionType type) => type switch
    {
        Linq.ExpressionType.ArrayLength or
        Linq.ExpressionType.Convert or
        Linq.ExpressionType.ConvertChecked or
        Linq.ExpressionType.Decrement or
        Linq.ExpressionType.Increment or
        Linq.ExpressionType.IsFalse or
        Linq.ExpressionType.IsTrue or
        Linq.ExpressionType.Negate or
        Linq.ExpressionType.NegateChecked or
        Linq.ExpressionType.Not or
        Linq.ExpressionType.OnesComplement or
        Linq.ExpressionType.PostDecrementAssign or
        Linq.ExpressionType.PostIncrementAssign or
        Linq.ExpressionType.PreDecrementAssign or
        Linq.ExpressionType.PreIncrementAssign or
        Linq.ExpressionType.Quote or
        Linq.ExpressionType.TypeAs or
        Linq.ExpressionType.UnaryPlus or
        Linq.ExpressionType.Unbox => new Unary(node)
        {
            ExpressionType = ExpressionType.Create(type, node.Graph),
        },

        _ => throw new Exception($"{type} is not unary"),
    };
}
