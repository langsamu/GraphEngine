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
        internal BaseBind(INode node)
            : base(node)
        {
        }

        public Member Member
        {
            get => this.GetRequired(BindMember, AsMember);

            set => this.SetRequired(BindMember, value);
        }

        public abstract Linq.MemberBinding LinqMemberBinding { get; }

        public static BaseBind Create(INode node, Linq.MemberBindingType kind)
        {
            return kind switch
            {
                Linq.MemberBindingType.Assignment => new Bind(node),
                Linq.MemberBindingType.MemberBinding => new MemberBind(node),
                Linq.MemberBindingType.ListBinding => new ListBind(node),

                _ => throw new Exception("{type} is not memberbind")
            };
        }

        internal static BaseBind Parse(INode node)
        {
            var type = Vocabulary.RdfType.ObjectOf(node);
            switch (type)
            {
                case INode t when t.Equals(Vocabulary.Bind): return new Bind(node);
                case INode t when t.Equals(Vocabulary.ListBind): return new ListBind(node);
                case INode t when t.Equals(Vocabulary.MemberBind): return new MemberBind(node);

                default: throw new Exception($"unknown bind type {type} on node {node}");
            }
        }
    }
}
