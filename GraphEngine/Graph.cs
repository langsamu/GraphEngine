// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VDS.RDF;
    using VDS.RDF.Ontology;
    using VDS.RDF.Query.Inference;
    using dotNetRDF = VDS.RDF;

    public class Graph : dotNetRDF.WrapperGraph
    {
        private static readonly IInferenceEngine Reasoner = new StaticRdfsReasoner();

        static Graph()
        {
            using var schemaFull = new NonIndexedGraph();
            schemaFull.LoadFromEmbeddedResource("GraphEngine.Resources.Schema.ttl, GraphEngine");

            using var schemaClean = new NonIndexedGraph();
            schemaClean.Assert(
                schemaFull
                    .GetTriplesWithPredicate(UriFactory.Create(OntologyHelper.PropertyDomain))
                    .Where(t => !ExcludedClasses.Contains(t.Object)));

            Reasoner.Initialise(schemaClean);
        }

        public Graph()
            : base()
        {
            this.AttachEventHandlers();
            this.TripleAsserted += this.Graph_TripleAsserted;
        }

        public Graph(IGraph g)
            : base(g)
        {
            Reasoner.Apply(this);
            this.TripleAsserted += this.Graph_TripleAsserted;
        }

        private static IEnumerable<INode> ExcludedClasses
        {
            get
            {
                yield return Vocabulary.Binary;
                yield return Vocabulary.Unary;
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
}
