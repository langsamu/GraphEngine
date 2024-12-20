// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using VDS.RDF.Ontology;
using VDS.RDF.Query.Inference;

public class Graph : WrapperGraph
{
    private static readonly StaticRdfsReasoner Reasoner = new();

    static Graph()
    {
        using var schemaFull = new NonIndexedGraph();
        schemaFull.LoadFromEmbeddedResource("GraphEngine.Resources.Schema.ttl, GraphEngine");

        using var schemaClean = new NonIndexedGraph();
        schemaClean.Assert(
            schemaFull
                .GetTriplesWithPredicate(VDS.RDF.UriFactory.Create(OntologyHelper.PropertyDomain))
                .Where(t => !ExcludedClasses.Contains(t.Object)));

        Reasoner.Initialise(schemaClean);
    }

    public Graph()
        : base()
    {
        AttachEventHandlers();
        TripleAsserted += Graph_TripleAsserted;
    }

    public Graph(IGraph g)
        : base(g)
    {
        Reasoner.Apply(this);
        TripleAsserted += Graph_TripleAsserted;
    }

    private static IEnumerable<INode> ExcludedClasses
    {
        get
        {
            yield return Vocabulary.BaseGoto;
            yield return Vocabulary.BaseBind;
        }
    }

    private void Graph_TripleAsserted(object sender, TripleEventArgs args)
    {
        using var g = new NonIndexedGraph();
        g.Assert(args.Triple);
        Reasoner.Apply(g, this);
    }
}
