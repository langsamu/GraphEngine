// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;

    public class Rethrow : Throw
    {
        [DebuggerStepThrough]
        public Rethrow(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.Rethrow;
        }
    }
}
