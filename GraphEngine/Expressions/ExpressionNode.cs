namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public abstract partial class ExpressionNode : WrapperNode
    {
        [DebuggerStepThrough]
        protected ExpressionNode(INode node) : base(node) { }

        public abstract Expression Expression { get; }

        // TODO: Handle non-expressions, e.g. uri node = System.Uri, blank node = new object()
        public static ExpressionNode Parse(INode node)
        {
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
