// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;

    public class Type : Node
    {
        [DebuggerStepThrough]
        internal Type(INode node)
            : base(node)
        {
        }

        public string Name => Required<string>(TypeName);

        public IEnumerable<Type> Arguments => List<Type>(TypeArguments);

        public System.Type SystemType
        {
            get
            {
                var t = System.Type.GetType(this.Name) ?? throw new InvalidOperationException($"Type {Name} not found.");

                if (t.IsGenericTypeDefinition)
                {
                    return t.MakeGenericType(this.Arguments.Select(arg => arg.SystemType).ToArray());
                }

                return t;
            }
        }
    }
}