// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Target : Node
    {
        private static readonly IDictionary<INode, Linq.LabelTarget> Cache = new Dictionary<INode, Linq.LabelTarget>();

        [DebuggerStepThrough]
        internal Target(INode node)
            : base(node)
        {
        }

        public Type Type => Optional<Type>(TargetType);

        public string Name => Optional<string>(TargetName);

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
    }
}