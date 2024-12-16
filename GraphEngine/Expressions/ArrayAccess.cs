// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class ArrayAccess : Expression
{
    [DebuggerStepThrough]
    internal ArrayAccess(NodeWithGraph node)
        : base(node)
    {
    }

    public Expression Array
    {
        get => this.GetRequired(ArrayAccessArray, Expression.Parse);

        set => this.SetRequired(ArrayAccessArray, value);
    }

    public ICollection<Expression> Indexes => this.Collection(ArrayAccessIndexes, Expression.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.ArrayAccess(this.Array.LinqExpression, this.Indexes.LinqExpressions());
}
