// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Rethrow : Throw
{
    [DebuggerStepThrough]
    public Rethrow(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.Rethrow;
}
