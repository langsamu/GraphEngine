// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

// TODO: Add overloads
public class MemberBind(NodeWithGraph node) : BaseBind(node, Vocabulary.MemberBind)
{
    public ICollection<BaseBind> Bindings => Collection(MemberBindBindings, BaseBind.Parse);

    public override Linq.MemberBinding LinqMemberBinding => Linq.Expression.MemberBind(
        Member.ReflectionMember,
        from binding in Bindings select binding.LinqMemberBinding);
}
