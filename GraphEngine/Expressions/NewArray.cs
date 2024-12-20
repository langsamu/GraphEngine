// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public abstract class NewArray(NodeWithGraph node, INode type) : Expression(node, type)
{
    protected delegate Linq.NewArrayExpression NewArrayExpressionFactory(System.Type type, IEnumerable<Linq.Expression> expressions);

    public Type Type
    {
        get => GetRequired(NewArrayType, Type.Parse);

        set => SetRequired(NewArrayType, value);
    }

    public ICollection<Expression> Expressions => Collection(NewArrayExpressions, Expression.Parse);

    public override Linq.Expression LinqExpression => FactoryMethod(Type.SystemType, Expressions.LinqExpressions());

    protected abstract NewArrayExpressionFactory FactoryMethod { get; }
}
