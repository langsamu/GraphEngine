﻿// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using VDS.RDF.Nodes;
    using static Vocabulary;

    public class Type : Node
    {
        [DebuggerStepThrough]
        internal Type(INode node)
            : base(node)
        {
        }

        // TODO: make simple
        public string Name
        {
            get
            {
                if (this.NodeType == NodeType.Literal)
                {
                    return this.AsValuedNode().AsString();
                }

                return Vocabulary.TypeName.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).Single();
            }
        }

        public IEnumerable<Type> Arguments => List<Type>(TypeArguments);

        public System.Type SystemType
        {
            get
            {
                var t = System.Type.GetType(this.Name) ?? throw new InvalidOperationException();

                if (t.IsGenericTypeDefinition)
                {
                    return t.MakeGenericType(this.Arguments.Select(arg => arg.SystemType).ToArray());
                }

                return t;
            }
        }
    }
}