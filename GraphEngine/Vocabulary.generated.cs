namespace GraphEngine
{
    using VDS.RDF;

    public static partial class Vocabulary
    {
        public static IUriNode Add { get; } = EngineNode("Add");

        public static IUriNode AddAssign { get; } = EngineNode("AddAssign");

        public static IUriNode AddAssignChecked { get; } = EngineNode("AddAssignChecked");

        public static IUriNode AddChecked { get; } = EngineNode("AddChecked");

        public static IUriNode And { get; } = EngineNode("And");

        public static IUriNode AndAlso { get; } = EngineNode("AndAlso");

        public static IUriNode AndAssign { get; } = EngineNode("AndAssign");

        public static IUriNode Arguments { get; } = EngineNode("arguments");

        public static IUriNode ArrayIndex { get; } = EngineNode("ArrayIndex");

        public static IUriNode ArrayLength { get; } = EngineNode("ArrayLength");

        public static IUriNode Assign { get; } = EngineNode("Assign");

        public static IUriNode Block { get; } = EngineNode("Block");

        public static IUriNode Body { get; } = EngineNode("body");

        public static IUriNode Bounds { get; } = EngineNode("bounds");

        public static IUriNode Break { get; } = EngineNode("Break");

        public static IUriNode BreakLabel { get; } = EngineNode("break");

        public static IUriNode Call { get; } = EngineNode("Call");

        public static IUriNode CatchVariable { get; } = EngineNode("variable");

        public static IUriNode Coalesce { get; } = EngineNode("Coalesce");

        public static IUriNode Condition { get; } = EngineNode("Condition");

        public static IUriNode Constant { get; } = EngineNode("Constant");

        public static IUriNode Continue { get; } = EngineNode("Continue");

        public static IUriNode ContinueLabel { get; } = EngineNode("continue");

        public static IUriNode Convert { get; } = EngineNode("Convert");

        public static IUriNode ConvertChecked { get; } = EngineNode("ConvertChecked");

        public static IUriNode Decrement { get; } = EngineNode("Decrement");

        public static IUriNode Default { get; } = EngineNode("Default");

        public static IUriNode Divide { get; } = EngineNode("Divide");

        public static IUriNode DivideAssign { get; } = EngineNode("DivideAssign");

        public static IUriNode Equal { get; } = EngineNode("Equal");

        public static IUriNode ExclusiveOr { get; } = EngineNode("ExclusiveOr");

        public static IUriNode ExclusiveOrAssign { get; } = EngineNode("ExclusiveOrAssign");

        public static IUriNode Expression { get; } = EngineNode("expression");

        public static IUriNode Expressions { get; } = EngineNode("expressions");

        public static IUriNode Fault { get; } = EngineNode("fault");

        public static IUriNode Filter { get; } = EngineNode("filter");

        public static IUriNode Finally { get; } = EngineNode("finally");

        public static IUriNode Goto { get; } = EngineNode("Goto");

        public static IUriNode GreaterThan { get; } = EngineNode("GreaterThan");

        public static IUriNode GreaterThanOrEqual { get; } = EngineNode("GreaterThanOrEqual");

        public static IUriNode Handlers { get; } = EngineNode("handlers");

        public static IUriNode IfFalse { get; } = EngineNode("ifFalse");

        public static IUriNode IfTrue { get; } = EngineNode("ifTrue");

        public static IUriNode Increment { get; } = EngineNode("Increment");

        public static IUriNode Instance { get; } = EngineNode("instance");

        public static IUriNode Invoke { get; } = EngineNode("Invoke");

        public static IUriNode IsFalse { get; } = EngineNode("IsFalse");

        public static IUriNode IsTrue { get; } = EngineNode("IsTrue");

        public static IUriNode Lambda { get; } = EngineNode("Lambda");

        public static IUriNode Left { get; } = EngineNode("left");

        public static IUriNode LeftShift { get; } = EngineNode("LeftShift");

        public static IUriNode LeftShiftAssign { get; } = EngineNode("LeftShiftAssign");

        public static IUriNode LessThan { get; } = EngineNode("LessThan");

        public static IUriNode LessThanOrEqual { get; } = EngineNode("LessThanOrEqual");

        public static IUriNode Loop { get; } = EngineNode("Loop");

        public static IUriNode Method { get; } = EngineNode("method");

        public static IUriNode Modulo { get; } = EngineNode("Modulo");

        public static IUriNode ModuloAssign { get; } = EngineNode("ModuloAssign");

        public static IUriNode Multiply { get; } = EngineNode("Multiply");

        public static IUriNode MultiplyAssign { get; } = EngineNode("MultiplyAssign");

        public static IUriNode MultiplyAssignChecked { get; } = EngineNode("MultiplyAssignChecked");

        public static IUriNode MultiplyChecked { get; } = EngineNode("MultiplyChecked");

        public static IUriNode Name { get; } = EngineNode("name");

        public static IUriNode Negate { get; } = EngineNode("Negate");

        public static IUriNode NegateChecked { get; } = EngineNode("NegateChecked");

        public static IUriNode New { get; } = EngineNode("New");

        public static IUriNode NewArrayBounds { get; } = EngineNode("NewArrayBounds");

        public static IUriNode Not { get; } = EngineNode("Not");

        public static IUriNode NotEqual { get; } = EngineNode("NotEqual");

        public static IUriNode OnesComplement { get; } = EngineNode("OnesComplement");

        public static IUriNode Operand { get; } = EngineNode("operand");

        public static IUriNode Or { get; } = EngineNode("Or");

        public static IUriNode OrAssign { get; } = EngineNode("OrAssign");

        public static IUriNode OrElse { get; } = EngineNode("OrElse");

        public static IUriNode Parameter { get; } = EngineNode("Parameter");

        public static IUriNode Parameters { get; } = EngineNode("parameters");

        public static IUriNode PostDecrementAssign { get; } = EngineNode("PostDecrementAssign");

        public static IUriNode PostIncrementAssign { get; } = EngineNode("PostIncrementAssign");

        public static IUriNode Power { get; } = EngineNode("Power");

        public static IUriNode PowerAssign { get; } = EngineNode("PowerAssign");

        public static IUriNode PreDecrementAssign { get; } = EngineNode("PreDecrementAssign");

        public static IUriNode PreIncrementAssign { get; } = EngineNode("PreIncrementAssign");

        public static IUriNode Quote { get; } = EngineNode("Quote");

        public static IUriNode Return { get; } = EngineNode("Return");

        public static IUriNode Right { get; } = EngineNode("right");

        public static IUriNode RightShift { get; } = EngineNode("RightShift");

        public static IUriNode RightShiftAssign { get; } = EngineNode("RightShiftAssign");

        public static IUriNode Subtract { get; } = EngineNode("Subtract");

        public static IUriNode SubtractAssign { get; } = EngineNode("SubtractAssign");

        public static IUriNode SubtractAssignChecked { get; } = EngineNode("SubtractAssignChecked");

        public static IUriNode SubtractChecked { get; } = EngineNode("SubtractChecked");

        public static IUriNode Target { get; } = EngineNode("target");

        public static IUriNode Test { get; } = EngineNode("test");

        public static IUriNode Throw { get; } = EngineNode("Throw");

        public static IUriNode Try { get; } = EngineNode("Try");

        public static IUriNode Type { get; } = EngineNode("type");

        public static IUriNode TypeArguments { get; } = EngineNode("typeArguments");

        public static IUriNode TypeAs { get; } = EngineNode("TypeAs");

        public static IUriNode UnaryPlus { get; } = EngineNode("UnaryPlus");

        public static IUriNode Unbox { get; } = EngineNode("Unbox");

        public static IUriNode Value { get; } = EngineNode("value");

        public static IUriNode Variable { get; } = EngineNode("Variable");

        public static IUriNode Variables { get; } = EngineNode("variables");
    }
}
