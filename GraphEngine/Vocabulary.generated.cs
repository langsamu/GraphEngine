// MIT License, Copyright 2019 Samu Lang

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

        public static IUriNode ArrayAccess { get; } = EngineNode("ArrayAccess");

        public static IUriNode ArrayIndex { get; } = EngineNode("ArrayIndex");

        public static IUriNode ArrayLength { get; } = EngineNode("ArrayLength");

        public static IUriNode Assign { get; } = EngineNode("Assign");

        public static IUriNode Bind { get; } = EngineNode("Bind");

        public static IUriNode Block { get; } = EngineNode("Block");

        public static IUriNode Break { get; } = EngineNode("Break");

        public static IUriNode Call { get; } = EngineNode("Call");

        public static IUriNode Case { get; } = EngineNode("Case");

        public static IUriNode Catch { get; } = EngineNode("Catch");

        public static IUriNode Coalesce { get; } = EngineNode("Coalesce");

        public static IUriNode Condition { get; } = EngineNode("Condition");

        public static IUriNode Constant { get; } = EngineNode("Constant");

        public static IUriNode Continue { get; } = EngineNode("Continue");

        public static IUriNode Convert { get; } = EngineNode("Convert");

        public static IUriNode ConvertChecked { get; } = EngineNode("ConvertChecked");

        public static IUriNode DebugInfo { get; } = EngineNode("DebugInfo");

        public static IUriNode Decrement { get; } = EngineNode("Decrement");

        public static IUriNode Default { get; } = EngineNode("Default");

        public static IUriNode Divide { get; } = EngineNode("Divide");

        public static IUriNode DivideAssign { get; } = EngineNode("DivideAssign");

        public static IUriNode Dynamic { get; } = EngineNode("Dynamic");

        public static IUriNode ElementInit { get; } = EngineNode("ElementInit");

        public static IUriNode Empty { get; } = EngineNode("Empty");

        public static IUriNode Equal { get; } = EngineNode("Equal");

        public static IUriNode ExclusiveOr { get; } = EngineNode("ExclusiveOr");

        public static IUriNode ExclusiveOrAssign { get; } = EngineNode("ExclusiveOrAssign");

        public static IUriNode Field { get; } = EngineNode("Field");

        public static IUriNode Goto { get; } = EngineNode("Goto");

        public static IUriNode GreaterThan { get; } = EngineNode("GreaterThan");

        public static IUriNode GreaterThanOrEqual { get; } = EngineNode("GreaterThanOrEqual");

        public static IUriNode IfThen { get; } = EngineNode("IfThen");

        public static IUriNode IfThenElse { get; } = EngineNode("IfThenElse");

        public static IUriNode Increment { get; } = EngineNode("Increment");

        public static IUriNode Invoke { get; } = EngineNode("Invoke");

        public static IUriNode IsFalse { get; } = EngineNode("IsFalse");

        public static IUriNode IsTrue { get; } = EngineNode("IsTrue");

        public static IUriNode Label { get; } = EngineNode("Label");

        public static IUriNode Lambda { get; } = EngineNode("Lambda");

        public static IUriNode LeftShift { get; } = EngineNode("LeftShift");

        public static IUriNode LeftShiftAssign { get; } = EngineNode("LeftShiftAssign");

        public static IUriNode LessThan { get; } = EngineNode("LessThan");

        public static IUriNode LessThanOrEqual { get; } = EngineNode("LessThanOrEqual");

        public static IUriNode ListBind { get; } = EngineNode("ListBind");

        public static IUriNode ListInit { get; } = EngineNode("ListInit");

        public static IUriNode Loop { get; } = EngineNode("Loop");

        public static IUriNode Member { get; } = EngineNode("Member");

        public static IUriNode MemberBind { get; } = EngineNode("MemberBind");

        public static IUriNode MemberInit { get; } = EngineNode("MemberInit");

        public static IUriNode Method { get; } = EngineNode("Method");

        public static IUriNode Modulo { get; } = EngineNode("Modulo");

        public static IUriNode ModuloAssign { get; } = EngineNode("ModuloAssign");

        public static IUriNode Multiply { get; } = EngineNode("Multiply");

        public static IUriNode MultiplyAssign { get; } = EngineNode("MultiplyAssign");

        public static IUriNode MultiplyAssignChecked { get; } = EngineNode("MultiplyAssignChecked");

        public static IUriNode MultiplyChecked { get; } = EngineNode("MultiplyChecked");

        public static IUriNode Negate { get; } = EngineNode("Negate");

        public static IUriNode NegateChecked { get; } = EngineNode("NegateChecked");

        public static IUriNode New { get; } = EngineNode("New");

        public static IUriNode NewArrayBounds { get; } = EngineNode("NewArrayBounds");

        public static IUriNode NewArrayInit { get; } = EngineNode("NewArrayInit");

        public static IUriNode Not { get; } = EngineNode("Not");

        public static IUriNode NotEqual { get; } = EngineNode("NotEqual");

        public static IUriNode OnesComplement { get; } = EngineNode("OnesComplement");

        public static IUriNode Or { get; } = EngineNode("Or");

        public static IUriNode OrAssign { get; } = EngineNode("OrAssign");

        public static IUriNode OrElse { get; } = EngineNode("OrElse");

        public static IUriNode Parameter { get; } = EngineNode("Parameter");

        public static IUriNode PostDecrementAssign { get; } = EngineNode("PostDecrementAssign");

        public static IUriNode PostIncrementAssign { get; } = EngineNode("PostIncrementAssign");

        public static IUriNode Power { get; } = EngineNode("Power");

        public static IUriNode PowerAssign { get; } = EngineNode("PowerAssign");

        public static IUriNode PreDecrementAssign { get; } = EngineNode("PreDecrementAssign");

        public static IUriNode PreIncrementAssign { get; } = EngineNode("PreIncrementAssign");

        public static IUriNode Property { get; } = EngineNode("Property");

        public static IUriNode PropertyOrField { get; } = EngineNode("PropertyOrField");

        public static IUriNode Quote { get; } = EngineNode("Quote");

        public static IUriNode ReferenceEqual { get; } = EngineNode("ReferenceEqual");

        public static IUriNode ReferenceNotEqual { get; } = EngineNode("ReferenceNotEqual");

        public static IUriNode Rethrow { get; } = EngineNode("Rethrow");

        public static IUriNode Return { get; } = EngineNode("Return");

        public static IUriNode RightShift { get; } = EngineNode("RightShift");

        public static IUriNode RightShiftAssign { get; } = EngineNode("RightShiftAssign");

        public static IUriNode RuntimeVariables { get; } = EngineNode("RuntimeVariables");

        public static IUriNode Subtract { get; } = EngineNode("Subtract");

        public static IUriNode SubtractAssign { get; } = EngineNode("SubtractAssign");

        public static IUriNode SubtractAssignChecked { get; } = EngineNode("SubtractAssignChecked");

        public static IUriNode SubtractChecked { get; } = EngineNode("SubtractChecked");

        public static IUriNode Switch { get; } = EngineNode("Switch");

        public static IUriNode Throw { get; } = EngineNode("Throw");

        public static IUriNode Try { get; } = EngineNode("Try");

        public static IUriNode Type { get; } = EngineNode("Type");

        public static IUriNode TypeAs { get; } = EngineNode("TypeAs");

        public static IUriNode UnaryPlus { get; } = EngineNode("UnaryPlus");

        public static IUriNode Unbox { get; } = EngineNode("Unbox");

        public static IUriNode Variable { get; } = EngineNode("Variable");

        public static IUriNode ArrayAccessArray { get; } = EngineNode("arrayAccessArray");

        public static IUriNode ArrayAccessIndexes { get; } = EngineNode("arrayAccessIndexes");

        public static IUriNode ArrayIndexArray { get; } = EngineNode("arrayIndexArray");

        public static IUriNode ArrayIndexIndex { get; } = EngineNode("arrayIndexIndex");

        public static IUriNode ArrayIndexIndexes { get; } = EngineNode("arrayIndexIndexes");

        public static IUriNode BinaryLeft { get; } = EngineNode("binaryLeft");

        public static IUriNode BinaryRight { get; } = EngineNode("binaryRight");

        public static IUriNode BindExpression { get; } = EngineNode("bindExpression");

        public static IUriNode BindMember { get; } = EngineNode("bindMember");

        public static IUriNode BlockExpressions { get; } = EngineNode("blockExpressions");

        public static IUriNode BlockVariables { get; } = EngineNode("blockVariables");

        public static IUriNode CallArguments { get; } = EngineNode("callArguments");

        public static IUriNode CallInstance { get; } = EngineNode("callInstance");

        public static IUriNode CallMethod { get; } = EngineNode("callMethod");

        public static IUriNode CallType { get; } = EngineNode("callType");

        public static IUriNode CallTypeArguments { get; } = EngineNode("callTypeArguments");

        public static IUriNode CaseBody { get; } = EngineNode("caseBody");

        public static IUriNode CaseTestValues { get; } = EngineNode("caseTestValues");

        public static IUriNode CatchBody { get; } = EngineNode("catchBody");

        public static IUriNode CatchFilter { get; } = EngineNode("catchFilter");

        public static IUriNode CatchType { get; } = EngineNode("catchType");

        public static IUriNode CatchVariable { get; } = EngineNode("catchVariable");

        public static IUriNode ConditionIfFalse { get; } = EngineNode("conditionIfFalse");

        public static IUriNode ConditionIfTrue { get; } = EngineNode("conditionIfTrue");

        public static IUriNode ConditionTest { get; } = EngineNode("conditionTest");

        public static IUriNode ConditionType { get; } = EngineNode("conditionType");

        public static IUriNode ConstantType { get; } = EngineNode("constantType");

        public static IUriNode ConstantValue { get; } = EngineNode("constantValue");

        public static IUriNode DefaultType { get; } = EngineNode("defaultType");

        public static IUriNode ElementInitAddMethod { get; } = EngineNode("elementInitAddMethod");

        public static IUriNode ElementInitArguments { get; } = EngineNode("elementInitArguments");

        public static IUriNode GotoTarget { get; } = EngineNode("gotoTarget");

        public static IUriNode GotoType { get; } = EngineNode("gotoType");

        public static IUriNode GotoValue { get; } = EngineNode("gotoValue");

        public static IUriNode InvokeArguments { get; } = EngineNode("invokeArguments");

        public static IUriNode InvokeExpression { get; } = EngineNode("invokeExpression");

        public static IUriNode LabelDefaultValue { get; } = EngineNode("labelDefaultValue");

        public static IUriNode LabelTarget { get; } = EngineNode("labelTarget");

        public static IUriNode LambdaBody { get; } = EngineNode("lambdaBody");

        public static IUriNode LambdaParameters { get; } = EngineNode("lambdaParameters");

        public static IUriNode ListBindInitializers { get; } = EngineNode("listBindInitializers");

        public static IUriNode LoopBody { get; } = EngineNode("loopBody");

        public static IUriNode LoopBreak { get; } = EngineNode("loopBreak");

        public static IUriNode LoopContinue { get; } = EngineNode("loopContinue");

        public static IUriNode MemberBindBindings { get; } = EngineNode("memberBindBindings");

        public static IUriNode MemberInitBindings { get; } = EngineNode("memberInitBindings");

        public static IUriNode MemberInitNewExpression { get; } = EngineNode("memberInitNewExpression");

        public static IUriNode MemberType { get; } = EngineNode("memberType");

        public static IUriNode MemberName { get; } = EngineNode("memberName");

        public static IUriNode NewArguments { get; } = EngineNode("newArguments");

        public static IUriNode NewArrayBoundsBounds { get; } = EngineNode("newArrayBoundsBounds");

        public static IUriNode NewArrayBoundsType { get; } = EngineNode("newArrayBoundsType");

        public static IUriNode NewType { get; } = EngineNode("newType");

        public static IUriNode ParameterName { get; } = EngineNode("parameterName");

        public static IUriNode ParameterType { get; } = EngineNode("parameterType");

        public static IUriNode SwitchCases { get; } = EngineNode("switchCases");

        public static IUriNode SwitchComparison { get; } = EngineNode("switchComparison");

        public static IUriNode SwitchDefaultBody { get; } = EngineNode("switchDefaultBody");

        public static IUriNode SwitchSwitchValue { get; } = EngineNode("switchSwitchValue");

        public static IUriNode SwitchType { get; } = EngineNode("switchType");

        public static IUriNode TargetName { get; } = EngineNode("targetName");

        public static IUriNode TargetType { get; } = EngineNode("targetType");

        public static IUriNode TryBody { get; } = EngineNode("tryBody");

        public static IUriNode TryFault { get; } = EngineNode("tryFault");

        public static IUriNode TryFinally { get; } = EngineNode("tryFinally");

        public static IUriNode TryHandlers { get; } = EngineNode("tryHandlers");

        public static IUriNode TryType { get; } = EngineNode("tryType");

        public static IUriNode TypeArguments { get; } = EngineNode("typeArguments");

        public static IUriNode TypeName { get; } = EngineNode("typeName");

        public static IUriNode UnaryOperand { get; } = EngineNode("unaryOperand");

        public static IUriNode UnaryType { get; } = EngineNode("unaryType");
    }
}
