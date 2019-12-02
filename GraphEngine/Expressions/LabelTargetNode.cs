namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class LabelTargetNode : WrapperNode
    {
        private static IDictionary<INode, LabelTarget> cache = new Dictionary<INode, LabelTarget>();

        [DebuggerStepThrough]
        private LabelTargetNode(INode node) : base(node) { }

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public string Name => Vocabulary.Name.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).SingleOrDefault();

        public LabelTarget LabelTarget
        {
            get
            {
                if (!cache.TryGetValue(this, out var label))
                {
                    var type = Type;
                    var name = Name;

                    if (type is object && name is object)
                    {
                        label = Expression.Label(type.Type, name);
                    }
                    else if (type is object)
                    {
                        label = Expression.Label(type.Type);
                    }
                    else if (name is object)
                    {
                        label = Expression.Label(name);
                    }
                    else
                    {
                        label = Expression.Label();
                    }

                    cache[this] = label;
                }

                return label;
            }
        }

        internal static LabelTargetNode Parse(INode node) => new LabelTargetNode(node);
    }
}