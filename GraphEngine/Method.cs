// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Reflection;
    using VDS.RDF;

    public class Method : Member
    {
        [DebuggerStepThrough]
        internal Method(INode node)
            : base(node)
        {
        }

        public MethodInfo? ReflectionMethod => this.Type.SystemType.GetMethod(this.Name);
    }
}