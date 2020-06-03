// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using VDS.RDF;

    public static partial class Vocabulary
    {
        public static IUriNode ArrayAccess { get; } = EngineNode("ArrayAccess");

        public static IUriNode ArrayIndex { get; } = EngineNode("ArrayIndex");

        public static IUriNode BaseBind { get; } = EngineNode("BaseBind");

        public static IUriNode BaseGoto { get; } = EngineNode("BaseGoto");

        public static IUriNode Binary { get; } = EngineNode("Binary");

        public static IUriNode BinaryOperation { get; } = EngineNode("BinaryOperation");

        public static IUriNode Bind { get; } = EngineNode("Bind");

        public static IUriNode Block { get; } = EngineNode("Block");

        public static IUriNode Break { get; } = EngineNode("Break");

        public static IUriNode Call { get; } = EngineNode("Call");

        public static IUriNode Case { get; } = EngineNode("Case");

        public static IUriNode Catch { get; } = EngineNode("Catch");

        public static IUriNode ClearDebugInfo { get; } = EngineNode("ClearDebugInfo");

        public static IUriNode Condition { get; } = EngineNode("Condition");

        public static IUriNode Constant { get; } = EngineNode("Constant");

        public static IUriNode Continue { get; } = EngineNode("Continue");

        public static IUriNode DebugInfo { get; } = EngineNode("DebugInfo");

        public static IUriNode Default { get; } = EngineNode("Default");

        public static IUriNode Dynamic { get; } = EngineNode("Dynamic");

        public static IUriNode ElementInit { get; } = EngineNode("ElementInit");

        public static IUriNode Empty { get; } = EngineNode("Empty");

        public static IUriNode Field { get; } = EngineNode("Field");

        public static IUriNode Goto { get; } = EngineNode("Goto");

        public static IUriNode IfThen { get; } = EngineNode("IfThen");

        public static IUriNode IfThenElse { get; } = EngineNode("IfThenElse");

        public static IUriNode Invoke { get; } = EngineNode("Invoke");

        public static IUriNode InvokeMember { get; } = EngineNode("InvokeMember");

        public static IUriNode Label { get; } = EngineNode("Label");

        public static IUriNode Lambda { get; } = EngineNode("Lambda");

        public static IUriNode ListBind { get; } = EngineNode("ListBind");

        public static IUriNode ListInit { get; } = EngineNode("ListInit");

        public static IUriNode Loop { get; } = EngineNode("Loop");

        public static IUriNode Member { get; } = EngineNode("Member");

        public static IUriNode MemberBind { get; } = EngineNode("MemberBind");

        public static IUriNode MemberInit { get; } = EngineNode("MemberInit");

        public static IUriNode Method { get; } = EngineNode("Method");

        public static IUriNode New { get; } = EngineNode("New");

        public static IUriNode NewArrayBounds { get; } = EngineNode("NewArrayBounds");

        public static IUriNode NewArrayInit { get; } = EngineNode("NewArrayInit");

        public static IUriNode Parameter { get; } = EngineNode("Parameter");

        public static IUriNode Property { get; } = EngineNode("Property");

        public static IUriNode PropertyOrField { get; } = EngineNode("PropertyOrField");

        public static IUriNode ReferenceEqual { get; } = EngineNode("ReferenceEqual");

        public static IUriNode ReferenceNotEqual { get; } = EngineNode("ReferenceNotEqual");

        public static IUriNode Rethrow { get; } = EngineNode("Rethrow");

        public static IUriNode Return { get; } = EngineNode("Return");

        public static IUriNode RuntimeVariables { get; } = EngineNode("RuntimeVariables");

        public static IUriNode Switch { get; } = EngineNode("Switch");

        public static IUriNode Target { get; } = EngineNode("Target");

        public static IUriNode Throw { get; } = EngineNode("Throw");

        public static IUriNode Try { get; } = EngineNode("Try");

        public static IUriNode Type { get; } = EngineNode("Type");

        public static IUriNode TypeBinary { get; } = EngineNode("TypeBinary");

        public static IUriNode Unary { get; } = EngineNode("Unary");

        public static IUriNode Variable { get; } = EngineNode("Variable");

        public static IUriNode ArrayAccessArray { get; } = EngineNode("arrayAccessArray");

        public static IUriNode ArrayAccessIndexes { get; } = EngineNode("arrayAccessIndexes");

        public static IUriNode ArrayIndexArray { get; } = EngineNode("arrayIndexArray");

        public static IUriNode ArrayIndexIndex { get; } = EngineNode("arrayIndexIndex");

        public static IUriNode ArrayIndexIndexes { get; } = EngineNode("arrayIndexIndexes");

        public static IUriNode BinaryConversion { get; } = EngineNode("binaryConversion");

        public static IUriNode BinaryExpressionType { get; } = EngineNode("binaryExpressionType");

        public static IUriNode BinaryLeft { get; } = EngineNode("binaryLeft");

        public static IUriNode BinaryLiftToNull { get; } = EngineNode("binaryLiftToNull");

        public static IUriNode BinaryMethod { get; } = EngineNode("binaryMethod");

        public static IUriNode BinaryRight { get; } = EngineNode("binaryRight");

        public static IUriNode BindExpression { get; } = EngineNode("bindExpression");

        public static IUriNode BindMember { get; } = EngineNode("bindMember");

        public static IUriNode BinderArguments { get; } = EngineNode("binderArguments");

        public static IUriNode BinderExpressionType { get; } = EngineNode("binderExpressionType");

        public static IUriNode BinderName { get; } = EngineNode("binderName");

        public static IUriNode BlockExpressions { get; } = EngineNode("blockExpressions");

        public static IUriNode BlockVariables { get; } = EngineNode("blockVariables");

        public static IUriNode CallArguments { get; } = EngineNode("callArguments");

        public static IUriNode CallInstance { get; } = EngineNode("callInstance");

        public static IUriNode CallMethod { get; } = EngineNode("callMethod");

        public static IUriNode CallMethodName { get; } = EngineNode("callMethodName");

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

        public static IUriNode DebugInfoDocument { get; } = EngineNode("debugInfoDocument");

        public static IUriNode DebugInfoEndColumn { get; } = EngineNode("debugInfoEndColumn");

        public static IUriNode DebugInfoEndLine { get; } = EngineNode("debugInfoEndLine");

        public static IUriNode DebugInfoStartColumn { get; } = EngineNode("debugInfoStartColumn");

        public static IUriNode DebugInfoStartLine { get; } = EngineNode("debugInfoStartLine");

        public static IUriNode DefaultType { get; } = EngineNode("defaultType");

        public static IUriNode DynamicArguments { get; } = EngineNode("dynamicArguments");

        public static IUriNode DynamicBinder { get; } = EngineNode("dynamicBinder");

        public static IUriNode DynamicReturnType { get; } = EngineNode("dynamicReturnType");

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

        public static IUriNode ListInitInitializers { get; } = EngineNode("listInitInitializers");

        public static IUriNode ListInitNewExpression { get; } = EngineNode("listInitNewExpression");

        public static IUriNode LoopBody { get; } = EngineNode("loopBody");

        public static IUriNode LoopBreak { get; } = EngineNode("loopBreak");

        public static IUriNode LoopContinue { get; } = EngineNode("loopContinue");

        public static IUriNode MemberAccessExpression { get; } = EngineNode("memberAccessExpression");

        public static IUriNode MemberAccessName { get; } = EngineNode("memberAccessName");

        public static IUriNode MemberAccessType { get; } = EngineNode("memberAccessType");

        public static IUriNode MemberBindBindings { get; } = EngineNode("memberBindBindings");

        public static IUriNode MemberInitBindings { get; } = EngineNode("memberInitBindings");

        public static IUriNode MemberInitNewExpression { get; } = EngineNode("memberInitNewExpression");

        public static IUriNode MemberType { get; } = EngineNode("memberType");

        public static IUriNode MemberName { get; } = EngineNode("memberName");

        public static IUriNode MethodTypeArguments { get; } = EngineNode("methodTypeArguments");

        public static IUriNode NewArguments { get; } = EngineNode("newArguments");

        public static IUriNode NewArrayExpressions { get; } = EngineNode("newArrayExpressions");

        public static IUriNode NewArrayType { get; } = EngineNode("newArrayType");

        public static IUriNode NewType { get; } = EngineNode("newType");

        public static IUriNode ParameterName { get; } = EngineNode("parameterName");

        public static IUriNode ParameterType { get; } = EngineNode("parameterType");

        public static IUriNode SwitchCases { get; } = EngineNode("switchCases");

        public static IUriNode PropertyArguments { get; } = EngineNode("propertyArguments");

        public static IUriNode RuntimeVariablesVariables { get; } = EngineNode("runtimeVariablesVariables");

        public static IUriNode SwitchComparison { get; } = EngineNode("switchComparison");

        public static IUriNode SwitchDefaultBody { get; } = EngineNode("switchDefaultBody");

        public static IUriNode SwitchSwitchValue { get; } = EngineNode("switchSwitchValue");

        public static IUriNode SwitchType { get; } = EngineNode("switchType");

        public static IUriNode SymbolDocumentFileName { get; } = EngineNode("symbolDocumentFileName");

        public static IUriNode SymbolDocumentLanguage { get; } = EngineNode("symbolDocumentLanguage");

        public static IUriNode SymbolDocumentLanguageVendor { get; } = EngineNode("symbolDocumentLanguageVendor");

        public static IUriNode SymbolDocumentDocumentType { get; } = EngineNode("symbolDocumentDocumentType");

        public static IUriNode TargetName { get; } = EngineNode("targetName");

        public static IUriNode TargetType { get; } = EngineNode("targetType");

        public static IUriNode ThrowType { get; } = EngineNode("throwType");

        public static IUriNode ThrowValue { get; } = EngineNode("throwValue");

        public static IUriNode TryBody { get; } = EngineNode("tryBody");

        public static IUriNode TryFault { get; } = EngineNode("tryFault");

        public static IUriNode TryFinally { get; } = EngineNode("tryFinally");

        public static IUriNode TryHandlers { get; } = EngineNode("tryHandlers");

        public static IUriNode TryType { get; } = EngineNode("tryType");

        public static IUriNode TypeArguments { get; } = EngineNode("typeArguments");

        public static IUriNode TypeBinaryExpression { get; } = EngineNode("typeBinaryExpression");

        public static IUriNode TypeBinaryExpressionType { get; } = EngineNode("typeBinaryExpressionType");

        public static IUriNode TypeBinaryType { get; } = EngineNode("typeBinaryType");

        public static IUriNode TypeName { get; } = EngineNode("typeName");

        public static IUriNode UnaryExpressionType { get; } = EngineNode("unaryExpressionType");

        public static IUriNode UnaryOperand { get; } = EngineNode("unaryOperand");

        public static IUriNode UnaryType { get; } = EngineNode("unaryType");
    }
}
