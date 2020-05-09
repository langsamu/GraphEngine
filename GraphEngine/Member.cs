// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using VDS.RDF;
    using static Vocabulary;

    // TODO: Implement
    public class Member : Node
    {
        [DebuggerStepThrough]
        internal Member(INode node)
            : base(node)
        {
        }

        public Type Type
        {
            get => this.GetRequired<Type>(MemberType);

            set => this.SetRequired(MemberType, value);
        }

        public string Name
        {
            get => this.GetRequired<string>(MemberName);

            set => this.SetRequired(MemberName, value);
        }

        public MemberInfo ReflectionMember => this.Type.SystemType.GetMember(this.Name).Single();
    }
}