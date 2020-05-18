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

        public INode? RdfType
        {
            get => Vocabulary.RdfType.ObjectOf(this);

            set => this.SetOptional(Vocabulary.RdfType, value);
        }

        internal static T Parse<T>(INode node)
            where T : class =>
            node switch
            {
                INode _ when typeof(T) == typeof(object) => (node.AsObject() as T)!,
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

        protected T? GetOptional<T>(INode predicate)
            where T : class =>
            predicate.ObjectsOf(this).Select(Parse<T>).SingleOrDefault();

        protected T GetRequired<T>(INode predicate)
            where T : class =>
            this.GetOptional<T>(predicate)
            ?? throw new Exception($"Single {predicate} not found on {this}");

        protected ICollection<T> Collection<T>(INode predicate)
            where T : class, INode =>
            new Collection<T>(this, predicate);

        protected void SetRequired(INode predicate, object @object) =>
            this.SetOptional(
                predicate,
                @object ?? throw new ArgumentNullException(nameof(@object)));

        protected void SetOptional(INode predicate, object? @object)
        {
            this.Graph.Retract(
                this.Graph.GetTriplesWithSubjectPredicate(
                    this,
                    predicate).ToList());

            if (@object is object)
            {
                this.Graph.Assert(this, predicate, @object.AsNode(this.Graph));
            }
        }
    }
}
