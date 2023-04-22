// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Ontology
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Class : Resource
    {
        [DebuggerStepThrough]
        public Class(NodeWithGraph node)
            : base(node)
        {
        }

        public IEnumerable<Resource> SubClassOf => Vocabulary.SubClassOf.ObjectsOf(this).Select(o => new Class(o));
    }
}
