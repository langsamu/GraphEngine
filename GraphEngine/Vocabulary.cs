// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using VDS.RDF;
    using VDS.RDF.Ontology;
    using VDS.RDF.Parsing;

    public static partial class Vocabulary
    {
        private const string BaseUriString = "http://example.com/";

        private static readonly NodeFactory Factory = new NodeFactory();

        public static Uri BaseUri => UriFactory.Create(BaseUriString);

        public static IUriNode Ontology { get; } = EngineNode("schema");

        public static IUriNode RdfType { get; } = AnyNode(RdfSpecsHelper.RdfType);

        public static IUriNode SubClassOf { get; } = AnyNode(OntologyHelper.PropertySubClassOf);

        public static IUriNode RdfsComment { get; } = AnyNode(OntologyHelper.PropertyComment);

        public static IUriNode RdfsDomain { get; } = AnyNode(OntologyHelper.PropertyDomain);

        public static IUriNode RdfsIsDefinedBy { get; } = AnyNode(OntologyHelper.PropertyIsDefinedBy);

        public static IUriNode RdfsLabel { get; } = AnyNode(OntologyHelper.PropertyLabel);

        public static IUriNode RdfsRange { get; } = AnyNode(OntologyHelper.PropertyRange);

        public static IUriNode OwlOntology { get; } = AnyNode(OntologyHelper.OwlOntology);

        public static IUriNode OwlClass { get; } = AnyNode(OntologyHelper.OwlClass);

        public static IUriNode OwlDatatypeProperty { get; } = AnyNode(OntologyHelper.OwlDatatypeProperty);

        public static IUriNode OwlObjectProperty { get; } = AnyNode(OntologyHelper.OwlObjectProperty);

        private static IUriNode EngineNode(string name) => AnyNode($"{BaseUriString}{name}");

        private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(UriFactory.Create(uri));

        public static class ExpressionTypes
        {
            private const string BaseUriString = "ExpressionTypes/";

            public static IUriNode Add { get; } = ExpressionTypeNode("Add");

            private static IUriNode ExpressionTypeNode(string name) => EngineNode($"{BaseUriString}{name}");
        }
    }
}
