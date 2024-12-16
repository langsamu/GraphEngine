// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Default : Expression
{
    [DebuggerStepThrough]
    internal Default(NodeWithGraph node)
        : base(node)
    {
    }

    public Type Type
    {
        get => this.GetRequired(DefaultType, Type.Parse);

        set => this.SetRequired(DefaultType, value);
    }

    public override Linq.Expression LinqExpression => Linq.Expression.Default(this.Type.SystemType);
}
