// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using VDS.RDF.Nodes;

    public class Type : WrapperNode
    {
        [DebuggerStepThrough]
        public Type(INode node)
            : base(node)
        {
        }

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

        public IEnumerable<Type> Arguments => Vocabulary.TypeArguments.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

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

        internal static Type Parse(INode node) => new Type(node);
    }
}