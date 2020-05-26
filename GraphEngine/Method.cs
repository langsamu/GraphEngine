// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
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

        internal static new Method Parse(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return new Method(node);
        }
    }
}