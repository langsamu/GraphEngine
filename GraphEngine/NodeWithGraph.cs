﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class NodeWithGraph : WrapperNode
{
    public NodeWithGraph(INode node, IGraph graph)
        : base(node)
        => this.Graph = graph;

    public IGraph Graph { get; }

    public INode Original => this.Node;
}
