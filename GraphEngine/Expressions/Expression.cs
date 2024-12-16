// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public abstract partial class Expression : Node
{
    [DebuggerStepThrough]
    protected Expression(NodeWithGraph node)
        : base(node)
    {
    }

    public abstract Linq.Expression LinqExpression { get; }

    // TODO: Handle non-expressions, e.g. uri node = System.Uri, blank node = new object()
    public static Expression Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        { NodeType: NodeType.Blank or NodeType.Uri } => ParseResource(node),
        _ => throw new Exception($"unknown node type {node.NodeType} on node {node}")
    };
}
