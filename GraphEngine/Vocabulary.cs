namespace GraphEngine
{
    using VDS.RDF;
    using VDS.RDF.Parsing;

    public static class Vocabulary
    {
        private const string BaseUri = "http://example.com/";
        private static readonly NodeFactory Factory = new NodeFactory();

        public static IUriNode Add { get; } = EngineNode("Add");

        public static IUriNode Subtract { get; } = EngineNode("Subtract");

        public static IUriNode Block { get; } = EngineNode("Block");

        public static IUriNode Lambda { get; } = EngineNode("Lambda");

        public static IUriNode Left { get; } = EngineNode("left");

        public static IUriNode Right { get; } = EngineNode("right");

        public static IUriNode Expressions { get; } = EngineNode("expressions");

        public static IUriNode Body { get; } = EngineNode("body");

        public static IUriNode RdfType { get; } = AnyNode(RdfSpecsHelper.RdfType);

        private static IUriNode EngineNode(string name) => AnyNode($"{BaseUri}{name}");

        private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(UriFactory.Create(uri));
    }
}
