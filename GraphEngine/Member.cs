// MIT License, Copyright 2019 Samu Lang

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

        public Type Type => Required<Type>(MemberType);

        public string Name => Required<string>(MemberName);

        public MemberInfo ReflectionMember => this.Type.SystemType.GetMember(this.Name).Single();
    }
}