// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class ListBind(NodeWithGraph node) : BaseBind(node, Vocabulary.ListBind)
{
    public ICollection<ElementInit> Initializers => this.Collection(ListBindInitializers, ElementInit.Parse);

    public override Linq.MemberBinding LinqMemberBinding => Linq.Expression.ListBind(
        this.Member.ReflectionMember,
        from initializer in this.Initializers select initializer.LinqElementInit);
}
