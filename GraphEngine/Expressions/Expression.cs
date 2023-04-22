// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public abstract partial class Expression : Node
    {
        [DebuggerStepThrough]
        protected Expression(NodeWithGraph node)
            : base(node)
        {
        }

        public abstract Linq.Expression LinqExpression { get; }

        // TODO: Handle non-expressions, e.g. uri node = System.Uri, blank node = new object()
        public static Expression Parse(NodeWithGraph node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var nodeType = node.NodeType;
            switch (nodeType)
            {
                case NodeType.Blank:
                case NodeType.Uri:
                    return ParseResource(node);

                default:
                    throw new Exception($"unknown node type {nodeType} on node {node}");
            }
        }
    }
}
