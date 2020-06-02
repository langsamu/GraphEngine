// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using VDS.RDF;
    using static Vocabulary;

    public class Method : Member
    {
        [DebuggerStepThrough]
        internal Method(INode node)
            : base(node)
        {
        }

        public ICollection<Type> TypeArguments => this.Collection(MethodTypeArguments, Type.Parse);

        public MethodInfo? ReflectionMethod
        {
            get
            {
                var methodInfo = this.Type.SystemType.GetMethod(this.Name);

                var typeArguments = this.TypeArguments.Select(ta => ta.SystemType).ToArray();
                if (typeArguments.Any())
                {
                    methodInfo = methodInfo.MakeGenericMethod(typeArguments);
                }

                return methodInfo;
            }
        }

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
