// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Target : Node
    {
        private static readonly IDictionary<INode, Linq.LabelTarget> Cache = new Dictionary<INode, Linq.LabelTarget>();

        [DebuggerStepThrough]
        internal Target(NodeWithGraph node)
            : base(node)
        {
        }

        public Type? Type
        {
            get => this.GetOptional(TargetType, Type.Parse);

            set => this.SetOptional(TargetType, value);
        }

        public string? Name
        {
            get => this.GetOptional(TargetName, AsString);

            set => this.SetOptional(TargetName, value);
        }

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

        internal static Target Parse(NodeWithGraph node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return new Target(node);
        }
    }
}
