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

        public static IUriNode AddChecked { get; } = EngineNode("AddChecked");

        public static IUriNode SubtractChecked { get; } = EngineNode("SubtractChecked");

        public static IUriNode Multiply { get; } = EngineNode("Multiply");

        public static IUriNode MultiplyChecked { get; } = EngineNode("MultiplyChecked");

        public static IUriNode Divide { get; } = EngineNode("Divide");

        public static IUriNode Modulo { get; } = EngineNode("Modulo");

        public static IUriNode Power { get; } = EngineNode("Power");

        public static IUriNode And { get; } = EngineNode("And");

        public static IUriNode AndAlso { get; } = EngineNode("AndAlso");

        public static IUriNode Or { get; } = EngineNode("Or");

        public static IUriNode OrElse { get; } = EngineNode("OrElse");

        public static IUriNode LessThanOrEqual { get; } = EngineNode("LessThanOrEqual");

        public static IUriNode GreaterThan { get; } = EngineNode("GreaterThan");

        public static IUriNode GreaterThanOrEqual { get; } = EngineNode("GreaterThanOrEqual");

        public static IUriNode Equal { get; } = EngineNode("Equal");

        public static IUriNode NotEqual { get; } = EngineNode("NotEqual");

        public static IUriNode ExclusiveOr { get; } = EngineNode("ExclusiveOr");

        public static IUriNode Coalesce { get; } = EngineNode("Coalesce");

        public static IUriNode ArrayIndex { get; } = EngineNode("ArrayIndex");

        public static IUriNode LeftShift { get; } = EngineNode("LeftShift");

        public static IUriNode RightShift { get; } = EngineNode("RightShift");

        public static IUriNode AddAssign { get; } = EngineNode("AddAssign");

        public static IUriNode AndAssign { get; } = EngineNode("AndAssign");

        public static IUriNode DivideAssign { get; } = EngineNode("DivideAssign");

        public static IUriNode ExclusiveOrAssign { get; } = EngineNode("ExclusiveOrAssign");

        public static IUriNode LeftShiftAssign { get; } = EngineNode("LeftShiftAssign");

        public static IUriNode ModuloAssign { get; } = EngineNode("ModuloAssign");

        public static IUriNode MultiplyAssign { get; } = EngineNode("MultiplyAssign");

        public static IUriNode OrAssign { get; } = EngineNode("OrAssign");

        public static IUriNode PowerAssign { get; } = EngineNode("PowerAssign");

        public static IUriNode RightShiftAssign { get; } = EngineNode("RightShiftAssign");

        public static IUriNode SubtractAssign { get; } = EngineNode("SubtractAssign");

        public static IUriNode AddAssignChecked { get; } = EngineNode("AddAssignChecked");

        public static IUriNode SubtractAssignChecked { get; } = EngineNode("SubtractAssignChecked");

        public static IUriNode MultiplyAssignChecked { get; } = EngineNode("MultiplyAssignChecked");

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
