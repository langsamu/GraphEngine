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

        public static IUriNode New { get; } = EngineNode("New");

        public static IUriNode Assign { get; } = EngineNode("Assign");

        public static IUriNode Variable { get; } = EngineNode("Variable");

        public static IUriNode Call { get; } = EngineNode("Call");

        public static IUriNode Constant { get; } = EngineNode("Constant");

        public static IUriNode Parameter { get; } = EngineNode("Parameter");

        public static IUriNode Invoke { get; } = EngineNode("Invoke");

        public static IUriNode Condition { get; } = EngineNode("Condition");

        public static IUriNode LessThan { get; } = EngineNode("LessThan");

        public static IUriNode Left { get; } = EngineNode("left");

        public static IUriNode Right { get; } = EngineNode("right");

        public static IUriNode Expressions { get; } = EngineNode("expressions");

        public static IUriNode Body { get; } = EngineNode("body");

        public static IUriNode Arguments { get; } = EngineNode("arguments");

        public static IUriNode Type { get; } = EngineNode("type");

        public static IUriNode Instance { get; } = EngineNode("instance");

        public static IUriNode Method { get; } = EngineNode("method");

        public static IUriNode TypeArguments { get; } = EngineNode("typeArguments");

        public static IUriNode Value { get; } = EngineNode("value");

        public static IUriNode Name { get; } = EngineNode("name");

        public static IUriNode Variables { get; } = EngineNode("variables");

        public static IUriNode Expression { get; } = EngineNode("expression");

        public static IUriNode Test { get; } = EngineNode("test");

        public static IUriNode IfTrue { get; } = EngineNode("ifTrue");

        public static IUriNode IfFalse { get; } = EngineNode("ifFalse");

        public static IUriNode Parameters { get; } = EngineNode("parameters");

        public static IUriNode RdfType { get; } = AnyNode(RdfSpecsHelper.RdfType);

        private static IUriNode EngineNode(string name) => AnyNode($"{BaseUri}{name}");

        private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(UriFactory.Create(uri));
    }
}
