// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

// TODO: Add overloads
public class ListInit(NodeWithGraph node) : Expression(node)
{
    public New NewExpression
    {
        get => this.GetRequired(ListInitNewExpression, New.Parse);

        set => this.SetRequired(ListInitNewExpression, value);
    }

    public ICollection<ElementInit> Initializers => this.Collection(ListInitInitializers, ElementInit.Parse);

    public override Linq.Expression LinqExpression =>
        Linq.Expression.ListInit(
            this.NewExpression.LinqNewExpression,
            from i in this.Initializers select i.LinqElementInit);
}
