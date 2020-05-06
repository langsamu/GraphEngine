// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using VDS.RDF;
    using VDS.RDF.Parsing;

    public static partial class Vocabulary
    {
        private const string BaseUri = "http://example.com/";
        private static readonly NodeFactory Factory = new NodeFactory();

        public static IUriNode RdfType { get; } = AnyNode(RdfSpecsHelper.RdfType);

        private static IUriNode EngineNode(string name) => AnyNode($"{BaseUri}{name}");

        private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(UriFactory.Create(uri));
    }
}
