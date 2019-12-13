// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Target : WrapperNode
    {
        private static readonly IDictionary<INode, Linq.LabelTarget> Cache = new Dictionary<INode, Linq.LabelTarget>();

        [DebuggerStepThrough]
        private Target(INode node)
            : base(node)
        {
        }

        public Type Type => Vocabulary.TargetType.ObjectsOf(this).Select(Type.Parse).SingleOrDefault();

        public string Name => Vocabulary.TargetName.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).SingleOrDefault();

        public Linq.LabelTarget LinqTarget
        {
            get
            {
                if (!Cache.TryGetValue(this, out var label))
                {
                    var type = this.Type;
                    var name = this.Name;

                    if (type is object && name is object)
                    {
                        label = Linq.Expression.Label(type.SystemType, name);
                    }
                    else if (type is object)
                    {
                        label = Linq.Expression.Label(type.SystemType);
                    }
                    else if (name is object)
                    {
                        label = Linq.Expression.Label(name);
                    }
                    else
                    {
                        label = Linq.Expression.Label();
                    }

                    Cache[this] = label;
                }

                return label;
            }
        }

        internal static Target Parse(INode node) => new Target(node);
    }
}