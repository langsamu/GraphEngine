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

        private static readonly NodeFactory Factory = new (new NodeFactoryOptions());

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

            public static IUriNode AddAssign { get; } = ExpressionTypeNode("AddAssign");

            public static IUriNode AddAssignChecked { get; } = ExpressionTypeNode("AddAssignChecked");

            public static IUriNode AddChecked { get; } = ExpressionTypeNode("AddChecked");

            public static IUriNode And { get; } = ExpressionTypeNode("And");

            public static IUriNode AndAlso { get; } = ExpressionTypeNode("AndAlso");

            public static IUriNode AndAssign { get; } = ExpressionTypeNode("AndAssign");

            public static IUriNode ArrayLength { get; } = ExpressionTypeNode("ArrayLength");

            public static IUriNode ArrayIndex { get; } = ExpressionTypeNode("ArrayIndex");

            public static IUriNode Assign { get; } = ExpressionTypeNode("Assign");

            public static IUriNode Coalesce { get; } = ExpressionTypeNode("Coalesce");

            public static IUriNode Convert { get; } = ExpressionTypeNode("Convert");

            public static IUriNode ConvertChecked { get; } = ExpressionTypeNode("ConvertChecked");

            public static IUriNode Decrement { get; } = ExpressionTypeNode("Decrement");

            public static IUriNode Divide { get; } = ExpressionTypeNode("Divide");

            public static IUriNode DivideAssign { get; } = ExpressionTypeNode("DivideAssign");

            public static IUriNode Equal { get; } = ExpressionTypeNode("Equal");

            public static IUriNode ExclusiveOr { get; } = ExpressionTypeNode("ExclusiveOr");

            public static IUriNode ExclusiveOrAssign { get; } = ExpressionTypeNode("ExclusiveOrAssign");

            public static IUriNode GreaterThan { get; } = ExpressionTypeNode("GreaterThan");

            public static IUriNode GreaterThanOrEqual { get; } = ExpressionTypeNode("GreaterThanOrEqual");

            public static IUriNode Increment { get; } = ExpressionTypeNode("Increment");

            public static IUriNode IsFalse { get; } = ExpressionTypeNode("IsFalse");

            public static IUriNode IsTrue { get; } = ExpressionTypeNode("IsTrue");

            public static IUriNode LeftShift { get; } = ExpressionTypeNode("LeftShift");

            public static IUriNode LeftShiftAssign { get; } = ExpressionTypeNode("LeftShiftAssign");

            public static IUriNode LessThan { get; } = ExpressionTypeNode("LessThan");

            public static IUriNode LessThanOrEqual { get; } = ExpressionTypeNode("LessThanOrEqual");

            public static IUriNode Modulo { get; } = ExpressionTypeNode("Modulo");

            public static IUriNode ModuloAssign { get; } = ExpressionTypeNode("ModuloAssign");

            public static IUriNode Multiply { get; } = ExpressionTypeNode("Multiply");

            public static IUriNode MultiplyAssign { get; } = ExpressionTypeNode("MultiplyAssign");

            public static IUriNode MultiplyAssignChecked { get; } = ExpressionTypeNode("MultiplyAssignChecked");

            public static IUriNode MultiplyChecked { get; } = ExpressionTypeNode("MultiplyChecked");

            public static IUriNode Negate { get; } = ExpressionTypeNode("Negate");

            public static IUriNode NegateChecked { get; } = ExpressionTypeNode("NegateChecked");

            public static IUriNode Not { get; } = ExpressionTypeNode("Not");

            public static IUriNode NotEqual { get; } = ExpressionTypeNode("NotEqual");

            public static IUriNode OnesComplement { get; } = ExpressionTypeNode("OnesComplement");

            public static IUriNode Or { get; } = ExpressionTypeNode("Or");

            public static IUriNode OrAssign { get; } = ExpressionTypeNode("OrAssign");

            public static IUriNode OrElse { get; } = ExpressionTypeNode("OrElse");

            public static IUriNode PostDecrementAssign { get; } = ExpressionTypeNode("PostDecrementAssign");

            public static IUriNode PostIncrementAssign { get; } = ExpressionTypeNode("PostIncrementAssign");

            public static IUriNode Power { get; } = ExpressionTypeNode("Power");

            public static IUriNode PowerAssign { get; } = ExpressionTypeNode("PowerAssign");

            public static IUriNode PreDecrementAssign { get; } = ExpressionTypeNode("PreDecrementAssign");

            public static IUriNode PreIncrementAssign { get; } = ExpressionTypeNode("PreIncrementAssign");

            public static IUriNode Quote { get; } = ExpressionTypeNode("Quote");

            public static IUriNode RightShift { get; } = ExpressionTypeNode("RightShift");

            public static IUriNode RightShiftAssign { get; } = ExpressionTypeNode("RightShiftAssign");

            public static IUriNode Subtract { get; } = ExpressionTypeNode("Subtract");

            public static IUriNode SubtractAssign { get; } = ExpressionTypeNode("SubtractAssign");

            public static IUriNode SubtractAssignChecked { get; } = ExpressionTypeNode("SubtractAssignChecked");

            public static IUriNode SubtractChecked { get; } = ExpressionTypeNode("SubtractChecked");

            public static IUriNode TypeAs { get; } = ExpressionTypeNode("TypeAs");

            public static IUriNode TypeEqual { get; } = ExpressionTypeNode("TypeEqual");

            public static IUriNode TypeIs { get; } = ExpressionTypeNode("TypeIs");

            public static IUriNode UnaryPlus { get; } = ExpressionTypeNode("UnaryPlus");

            public static IUriNode Unbox { get; } = ExpressionTypeNode("Unbox");

            private static IUriNode ExpressionTypeNode(string name) => EngineNode($"{BaseUriString}{name}");
        }
    }
}
