// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class NodeWithGraph(INode node, IGraph graph) : WrapperNode(node)
{
    public IGraph Graph { get; } = graph;

    public INode Original => Node;
}
