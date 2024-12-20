// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Bind(NodeWithGraph node) : BaseBind(node, Vocabulary.Bind)
{
    public Expression Expression
    {
        get => GetRequired(BindExpression, Expression.Parse);

        set => SetRequired(BindExpression, value);
    }

    public override Linq.MemberBinding LinqMemberBinding => Linq.Expression.Bind(Member.ReflectionMember, Expression.LinqExpression);
}
