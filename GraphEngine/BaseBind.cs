// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public abstract class BaseBind : Node
    {
        [DebuggerStepThrough]
        internal BaseBind(NodeWithGraph node)
            : base(node)
        {
        }

        public Member Member
        {
            get => this.GetRequired(BindMember, Member.Parse);

            set => this.SetRequired(BindMember, value);
        }

        public abstract Linq.MemberBinding LinqMemberBinding { get; }

        public static BaseBind Create(NodeWithGraph node, Linq.MemberBindingType kind) => kind switch
        {
            Linq.MemberBindingType.Assignment => new Bind(node),
            Linq.MemberBindingType.MemberBinding => new MemberBind(node),
            Linq.MemberBindingType.ListBinding => new ListBind(node),

            _ => throw new Exception("{type} is not memberbind")
        };

        internal static BaseBind Parse(NodeWithGraph node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return Vocabulary.RdfType.ObjectOf(node) switch
            {
                INode t when t.Equals(Vocabulary.Bind) => new Bind(node),
                INode t when t.Equals(Vocabulary.ListBind) => new ListBind(node),
                INode t when t.Equals(Vocabulary.MemberBind) => new MemberBind(node),

                null => throw new Exception($"type not found on node {node}"),
                INode t => throw new Exception($"unknown bind type {t} on node {node}"),
            };
        }
    }
}
