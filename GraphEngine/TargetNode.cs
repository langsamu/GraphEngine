﻿// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class TargetNode : WrapperNode
    {
        private static readonly IDictionary<INode, LabelTarget> Cache = new Dictionary<INode, LabelTarget>();

        [DebuggerStepThrough]
        private TargetNode(INode node)
            : base(node)
        {
        }

        public TypeNode Type => Vocabulary.TargetType.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public string Name => Vocabulary.TargetName.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).SingleOrDefault();

        public LabelTarget LabelTarget
        {
            get
            {
                if (!Cache.TryGetValue(this, out var label))
                {
                    var type = this.Type;
                    var name = this.Name;

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

                    Cache[this] = label;
                }

                return label;
            }
        }

        internal static TargetNode Parse(INode node) => new TargetNode(node);
    }
}