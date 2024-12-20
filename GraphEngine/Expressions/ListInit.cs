// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

// TODO: Add overloads
public class ListInit(NodeWithGraph node) : Expression(node)
{
    public New NewExpression
    {
        get => GetRequired(ListInitNewExpression, New.Parse);

        set => SetRequired(ListInitNewExpression, value);
    }

    public ICollection<ElementInit> Initializers => Collection(ListInitInitializers, ElementInit.Parse);

    public override Linq.Expression LinqExpression =>
        Linq.Expression.ListInit(
            NewExpression.LinqNewExpression,
            from i in Initializers select i.LinqElementInit);
}
