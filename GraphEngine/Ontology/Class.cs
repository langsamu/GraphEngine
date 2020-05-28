// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Ontology
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;

    public class Class : Resource
    {
        [DebuggerStepThrough]
        public Class(INode node)
            : base(node)
        {
        }

        public IEnumerable<Resource> SubClassOf => Vocabulary.SubClassOf.ObjectsOf(this).Select(o => new Class(o));
    }
}
