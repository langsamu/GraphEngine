// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public abstract class NewArray : Expression
{
    [DebuggerStepThrough]
    internal NewArray(NodeWithGraph node)
        : base(node)
    {
    }

    protected delegate Linq.NewArrayExpression NewArrayExpressionFactory(System.Type type, IEnumerable<Linq.Expression> expressions);

    public Type Type
    {
        get => this.GetRequired(NewArrayType, Type.Parse);

        set => this.SetRequired(NewArrayType, value);
    }

    public ICollection<Expression> Expressions => this.Collection(NewArrayExpressions, Expression.Parse);

    public override Linq.Expression LinqExpression => this.FactoryMethod(this.Type.SystemType, this.Expressions.LinqExpressions());

    protected abstract NewArrayExpressionFactory FactoryMethod { get; }
}
