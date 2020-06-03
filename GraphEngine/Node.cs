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

        protected static int AsInt(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (!(node.NodeType == NodeType.Literal && node is ILiteralNode literalNode))
            {
                throw new InvalidOperationException($"{node} is not literal node");
            }

            if (!int.TryParse(literalNode.Value, out var i))
            {
                throw new InvalidOperationException($"{node} is not valid int");
            }

            return i;
        }

        protected static object AsObject(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return node.AsObject();
        }

        protected static string AsString(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (!(node.NodeType == NodeType.Literal && node is ILiteralNode literalNode))
            {
                throw new InvalidOperationException($"{node} is not literal node");
            }

            return literalNode.Value;
        }

        protected static Guid AsGuid(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (!(node.NodeType == NodeType.Uri && node is IUriNode uriNode))
            {
                throw new InvalidOperationException($"{node} is not uri node");
            }

            var uri = uriNode.Uri;

            if (uri.Scheme != "urn")
            {
                throw new InvalidOperationException($"{uri} is not URN");
            }

            const string uuid = "uuid:";
            var path = uri.AbsolutePath;
            if (!path.StartsWith(uuid, StringComparison.Ordinal))
            {
                throw new InvalidOperationException($"{uri} is not UUID URN");
            }

            if (!Guid.TryParse(path.AsSpan(uuid.Length), out var g))
            {
                throw new InvalidOperationException($"{path} is not valid UUID");
            }

            return g;
        }

        protected static bool AsBool(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (!(node.NodeType == NodeType.Literal && node is ILiteralNode literalNode))
            {
                throw new InvalidOperationException($"{node} is not literal node");
            }

            if (!bool.TryParse(literalNode.Value, out var b))
            {
                throw new InvalidOperationException($"{node} is not valid bool");
            }

            return b;
        }

        protected ICollection<T> Collection<T>(INode predicate, Func<INode, T> parser)
            where T : class, INode =>
            new Collection<T>(this, predicate, parser);

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

        protected T? GetOptional<T>(INode predicate, Func<INode, T> parser)
            where T : class =>
            predicate.ObjectsOf(this).Select(parser).SingleOrDefault();

        protected T? GetOptionalS<T>(INode predicate, Func<INode, T> parser)
            where T : struct
        {
            var enumerable = predicate.ObjectsOf(this).Select(parser);

            if (!enumerable.Any())
            {
                return null;
            }

            return enumerable.Single();
        }

        protected T GetRequired<T>(INode predicate, Func<INode, T> parser)
            where T : class =>
            this.GetOptional<T>(predicate, parser)
            ?? throw new Exception($"Single {predicate} not found on {this}");

        protected T GetRequiredS<T>(INode predicate, Func<INode, T> parser)
            where T : struct =>
            this.GetOptionalS<T>(predicate, parser)
            ?? throw new Exception($"Single {predicate} not found on {this}");
    }
}
