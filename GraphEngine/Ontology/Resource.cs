// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Ontology
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;

    public class Resource : NodeWithGraph
    {
        [DebuggerStepThrough]
        public Resource(NodeWithGraph node)
            : base(node, node.Graph)
        {
        }

        public Uri? Uri => (this as IUriNode)?.Uri;

        public IEnumerable<INode> Types => Vocabulary.RdfType.ObjectsOf(this);

        public IEnumerable<Resource> IsDefinedBy => Vocabulary.RdfsIsDefinedBy.ObjectsOf(this).Select(o => new Resource(o));

        public IEnumerable<INode> Labels => Vocabulary.RdfsLabel.ObjectsOf(this);

        public IEnumerable<INode> Comments => Vocabulary.RdfsComment.ObjectsOf(this);
    }
}
