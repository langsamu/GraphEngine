// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Bind(NodeWithGraph node) : BaseBind(node, Vocabulary.Bind)
{
    public Expression Expression
    {
        get => this.GetRequired(BindExpression, Expression.Parse);

        set => this.SetRequired(BindExpression, value);
    }

    public override Linq.MemberBinding LinqMemberBinding => Linq.Expression.Bind(this.Member.ReflectionMember, this.Expression.LinqExpression);
}
