// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

// TODO: Review why we must expose underlying node
public class NodeWithGraph(INode node, IGraph graph) : GraphWrapperNode(node, graph)
{
    public INode Original => Node;
}
