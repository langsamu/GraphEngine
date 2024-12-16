// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Ontology;

public class Property : Resource
{
    [DebuggerStepThrough]
    public Property(NodeWithGraph node)
        : base(node)
    {
    }

    public IEnumerable<Class> Domains => from o in Vocabulary.RdfsDomain.ObjectsOf(this) select new Class(o);

    public IEnumerable<Resource> Ranges => from o in Vocabulary.RdfsRange.ObjectsOf(this) select new Resource(o);
}
