namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using VDS.RDF.Nodes;

    public class TypeNode : WrapperNode
    {
        [DebuggerStepThrough]
        public TypeNode(INode node) : base(node) { }

        public string Name
        {
            get
            {
                if (NodeType == NodeType.Literal)
                {
                    return this.AsValuedNode().AsString();
                }

                return Vocabulary.Name.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).Single();
            }
        }

        public IEnumerable<TypeNode> TypeArguments => Vocabulary.TypeArguments.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(Parse);

        public Type Type
        {
            get
            {
                var t = Type.GetType(Name);

                if (t.IsGenericTypeDefinition)
                {
                    return t.MakeGenericType(TypeArguments.Select(arg => arg.Type).ToArray());
                }

                return t;
            }
        }

        internal static TypeNode Parse(INode node) => new TypeNode(node);
    }
}