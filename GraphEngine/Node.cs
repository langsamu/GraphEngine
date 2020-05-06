// MIT License, Copyright 2020 Samu Lang

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

        protected T? Optional<T>(INode predicate)
            where T : class
            => predicate.ObjectsOf(this).Select(Parse<T>).SingleOrDefault();

        protected T Required<T>(INode predicate)
            where T : class
            => Optional<T>(predicate)
            ?? throw new Exception($"Single {predicate} not found on {this}");

        protected IEnumerable<T> List<T>(INode predicate)
            where T : class
            => predicate.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse<T>);

        private static T Parse<T>(INode node)
            where T : class
        {
            return node switch
            {
                INode _ when typeof(Expression).IsAssignableFrom(typeof(T)) => (Expression.Parse(node) as T)!,
                INode _ when typeof(T) == typeof(BaseBind) => (BaseBind.Parse(node) as T)!,
                INode _ when typeof(T) == typeof(Case) => (new Case(node) as T)!,
                INode _ when typeof(T) == typeof(CatchBlock) => (new CatchBlock(node) as T)!,
                INode _ when typeof(T) == typeof(ElementInit) => (new ElementInit(node) as T)!,
                INode _ when typeof(T) == typeof(Member) => (new Member(node) as T)!,
                INode _ when typeof(T) == typeof(Method) => (new Method(node) as T)!,
                INode _ when typeof(T) == typeof(Parameter) => (new Parameter(node) as T)!,
                INode _ when typeof(T) == typeof(Target) => (new Target(node) as T)!,
                INode _ when typeof(T) == typeof(Type) => (new Type(node) as T)!,
                INode _ when typeof(T) == typeof(string) => (((ILiteralNode)node).Value as T)!,

                _ => throw new Exception($"unknown node {node}"),
            };
        }
    }
}
