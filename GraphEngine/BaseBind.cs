// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public abstract class BaseBind(NodeWithGraph node, INode type) : Node(node, type)
{
    public Member Member
    {
        get => GetRequired(BindMember, Member.Parse);

        set => SetRequired(BindMember, value);
    }

    public abstract Linq.MemberBinding LinqMemberBinding { get; }

    public static BaseBind Create(NodeWithGraph node, Linq.MemberBindingType kind) => kind switch
    {
        Linq.MemberBindingType.Assignment => new Bind(node),
        Linq.MemberBindingType.MemberBinding => new MemberBind(node),
        Linq.MemberBindingType.ListBinding => new ListBind(node),

        _ => throw new GraphEngineException("{type} is not memberbind")
    };

    internal static BaseBind Parse(NodeWithGraph node)
    {
        ArgumentNullException.ThrowIfNull(node);

        return Vocabulary.RdfType.ObjectOf(node) switch
        {
            INode t when t.Equals(Vocabulary.Bind) => new Bind(node),
            INode t when t.Equals(Vocabulary.ListBind) => new ListBind(node),
            INode t when t.Equals(Vocabulary.MemberBind) => new MemberBind(node),

            null => throw new GraphEngineException($"type not found on node {node}"),
            var t => throw new GraphEngineException($"unknown bind type {t} on node {node}"),
        };
    }
}
