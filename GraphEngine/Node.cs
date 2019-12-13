// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;

    public abstract partial class Node : WrapperNode
    {
        [DebuggerStepThrough]
        protected Node(INode node)
            : base(node)
        {
        }

        protected T Optional<T>(INode predicate)
            where T : class
            => predicate.ObjectsOf(this).Select(Parse<T>).SingleOrDefault();

        protected T Required<T>(INode predicate)
            where T : class
            => predicate.ObjectsOf(this).Select(Parse<T>).Single();

        protected IEnumerable<T> List<T>(INode predicate)
            where T : class
            => predicate.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse<T>);

        private static T Parse<T>(INode node)
            where T : class
        {
            if (typeof(T) == typeof(string))
            {
                return ((ILiteralNode)node).Value as T;
            }

            if (typeof(T) == typeof(Expression))
            {
                return Expression.Parse(node) as T;
            }

            if (typeof(T) == typeof(Parameter))
            {
                return Parameter.Parse(node) as T;
            }

            if (typeof(T) == typeof(Type))
            {
                return new Type(node) as T;
            }

            if (typeof(T) == typeof(Target))
            {
                return new Target(node) as T;
            }

            if (typeof(T) == typeof(CatchBlock))
            {
                return new CatchBlock(node) as T;
            }

            throw new Exception();
        }
    }
}
