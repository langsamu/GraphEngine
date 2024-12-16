// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

// TODO: Add overloads
public class MemberBind : BaseBind
{
    [DebuggerStepThrough]
    internal MemberBind(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.MemberBind;

    public ICollection<BaseBind> Bindings => this.Collection(MemberBindBindings, BaseBind.Parse);

    public override Linq.MemberBinding LinqMemberBinding => Linq.Expression.MemberBind(
        this.Member.ReflectionMember,
        from binding in this.Bindings select binding.LinqMemberBinding);
}
