// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Ontology
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;

    public class Property : Resource
    {
        [DebuggerStepThrough]
        public Property(INode node)
            : base(node)
        {
        }

        public IEnumerable<Resource> Domains => Vocabulary.RdfsDomain.ObjectsOf(this).Select(o => new Resource(o));

        public IEnumerable<Resource> Ranges => Vocabulary.RdfsRange.ObjectsOf(this).Select(o => new Resource(o));
    }
}