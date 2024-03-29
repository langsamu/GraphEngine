﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using static Vocabulary;

    public class Member : Node
    {
        [DebuggerStepThrough]
        internal Member(NodeWithGraph node)
            : base(node)
        {
        }

        public Type Type
        {
            get => this.GetRequired(MemberType, Type.Parse);

            set => this.SetRequired(MemberType, value);
        }

        public string Name
        {
            get => this.GetRequired(MemberName, AsString);

            set => this.SetRequired(MemberName, value);
        }

        public MemberInfo ReflectionMember => this.Type.SystemType.GetMember(this.Name).Single();

        internal static Member Parse(NodeWithGraph node) => node switch
        {
            null => throw new ArgumentNullException(nameof(node)),
            _ => new Member(node)
        };
    }
}
