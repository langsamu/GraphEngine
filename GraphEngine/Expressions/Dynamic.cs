// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Dynamic : Expression
{
    [DebuggerStepThrough]
    internal Dynamic(NodeWithGraph node)
        : base(node)
    {
    }

    public Binder Binder
    {
        get => this.GetRequired(DynamicBinder, Binder.Parse);

        set => this.SetRequired(DynamicBinder, value);
    }

    public Type ReturnType
    {
        get => this.GetRequired(DynamicReturnType, Type.Parse);

        set => this.SetRequired(DynamicReturnType, value);
    }

    public ICollection<Expression> Arguments => this.Collection(DynamicArguments, Expression.Parse);

    public override Linq.Expression LinqExpression =>
        Linq.Expression.Dynamic(
            this.Binder.SystemBinder,
            this.ReturnType.SystemType,
            this.Arguments.LinqExpressions());
}
