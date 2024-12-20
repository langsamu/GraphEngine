// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Catch(NodeWithGraph node) : Node(node)
{
    public Type? Type
    {
        get => GetOptional(CatchType, Type.Parse);

        set => SetOptional(CatchType, value);
    }

    public Expression Body
    {
        get => GetRequired(CatchBody, Expression.Parse);

        set => SetRequired(CatchBody, value);
    }

    public Parameter? Variable
    {
        get => GetOptional(CatchVariable, Parameter.Parse);

        set => SetOptional(CatchVariable, value);
    }

    public Expression? Filter
    {
        get => GetOptional(CatchFilter, Expression.Parse);

        set => SetOptional(CatchFilter, value);
    }

    public Linq.CatchBlock LinqCatchBlock
    {
        get
        {
            var variable = Variable?.LinqParameter;
            return Linq.Expression.MakeCatchBlock(Type?.SystemType ?? variable?.Type, variable, Body.LinqExpression, Filter?.LinqExpression);
        }
    }

    internal static Catch Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new Catch(node)
    };
}
