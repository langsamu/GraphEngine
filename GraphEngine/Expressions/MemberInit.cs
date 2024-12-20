// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class MemberInit(NodeWithGraph node) : Expression(node)
{
    public New NewExpression
    {
        get => GetRequired(MemberInitNewExpression, New.Parse);

        set => SetRequired(MemberInitNewExpression, value);
    }

    public ICollection<BaseBind> Bindings => Collection(MemberInitBindings, BaseBind.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.MemberInit(
        NewExpression.LinqNewExpression,
        from binding in Bindings select binding.LinqMemberBinding);
}
