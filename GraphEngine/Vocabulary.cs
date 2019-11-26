namespace GraphEngine
{
    using VDS.RDF;

    public static class Vocabulary
    {
        private const string BaseUri = "http://example.com/";
        private static readonly NodeFactory Factory = new NodeFactory();

        public static IUriNode Add { get; } = EngineNode("Add");

        public static IUriNode Left { get; } = EngineNode("left");

        public static IUriNode Right { get; } = EngineNode("right");

        private static IUriNode EngineNode(string name) => AnyNode($"{BaseUri}{name}");

        private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(UriFactory.Create(uri));
    }
}
