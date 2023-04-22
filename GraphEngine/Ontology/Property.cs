// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Ontology
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Property : Resource
    {
        [DebuggerStepThrough]
        public Property(NodeWithGraph node)
            : base(node)
        {
        }

        public IEnumerable<Class> Domains => Vocabulary.RdfsDomain.ObjectsOf(this).Select(o => new Class(o));

        public IEnumerable<Resource> Ranges => Vocabulary.RdfsRange.ObjectsOf(this).Select(o => new Resource(o));
    }
}
