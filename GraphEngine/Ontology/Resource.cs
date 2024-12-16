// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Ontology;

public class Resource(NodeWithGraph node) : NodeWithGraph(node, node.Graph)
{
    public Uri? Uri => (this as IUriNode)?.Uri;

    public IEnumerable<INode> Types => Vocabulary.RdfType.ObjectsOf(this);

    public IEnumerable<Resource> IsDefinedBy => from o in Vocabulary.RdfsIsDefinedBy.ObjectsOf(this) select new Resource(o);

    public IEnumerable<INode> Labels => Vocabulary.RdfsLabel.ObjectsOf(this);

    public IEnumerable<INode> Comments => Vocabulary.RdfsComment.ObjectsOf(this);
}
