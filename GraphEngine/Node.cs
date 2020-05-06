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
        {
            var t = predicate.ObjectsOf(this).Select(Parse<T>).SingleOrDefault();

            if (t is null)
            {
                throw new Exception($"Single {predicate} not found on {this}");
            }

            return t;
        }

        protected IEnumerable<T> List<T>(INode predicate)
            where T : class
            => predicate.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse<T>);

        private static T Parse<T>(INode node)
            where T : class
        {
            switch (node)
            {
                case INode _ when typeof(Expression).IsAssignableFrom(typeof(T)): return Expression.Parse(node) as T;
                case INode _ when typeof(T) == typeof(BaseBind): return BaseBind.Parse(node) as T;
                case INode _ when typeof(T) == typeof(CatchBlock): return new CatchBlock(node) as T;
                case INode _ when typeof(T) == typeof(ElementInit): return new ElementInit(node) as T;
                case INode _ when typeof(T) == typeof(Member): return new Member(node) as T;
                case INode _ when typeof(T) == typeof(Method): return new Method(node) as T;
                case INode _ when typeof(T) == typeof(Parameter): return new Parameter(node) as T;
                case INode _ when typeof(T) == typeof(Target): return new Target(node) as T;
                case INode _ when typeof(T) == typeof(Type): return new Type(node) as T;
                case INode _ when typeof(T) == typeof(string): return ((ILiteralNode)node).Value as T;

                default: throw new Exception($"unknown node {node}");
            }
        }
    }
}
