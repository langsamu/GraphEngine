// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using FluentAssertions;
    using GraphEngine.Ontology;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using VDS.RDF.Parsing;

    [TestClass]
    public class SchemaTests
    {
        private static readonly Ontology.Graph OntologyGraph = new Ontology.Graph();
        private static string ontologyString;

        private static IEnumerable<object[]> Ontologies => OntologyGraph.Ontologies.Select(o => new[] { o });

        private static IEnumerable<object[]> Properties => OntologyGraph.DatatypeProperties.Union(OntologyGraph.ObjectProperties).Select(p => new[] { p });

        private static IEnumerable<object[]> ObjectProperties => OntologyGraph.ObjectProperties.Select(p => new[] { p });

        private static IEnumerable<object[]> Classes => OntologyGraph.Classes.Select(c => new[] { c });

        private static IEnumerable<object[]> Resources => Ontologies.Union(Classes).Union(Properties);

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            OntologyGraph.LoadFromEmbeddedResource("GraphEngine.Resources.Schema.ttl, GraphEngine");

            using var reader = new StreamReader(typeof(Ontology.Graph).Assembly.GetManifestResourceStream("GraphEngine.Resources.Schema.ttl"));
            ontologyString = reader.ReadToEnd();
        }

        [TestMethod]
        [DynamicData(nameof(Classes))]
        public void Class_name_is_in_pascal_case(Class @class)
        {
            Vocabulary.BaseUri.MakeRelativeUri(@class.Uri).ToString().Should().MatchRegex(@"^[A-Z]([a-z]|[A-Z]|[0-9])*$", "classes must be PascalCased");
        }

        [TestMethod]
        [DynamicData(nameof(Classes))]
        public void Class_superclasses_are_classes(Class @class)
        {
            foreach (var superclass in @class.SubClassOf.Where(Is_from_namespace))
            {
                superclass.Should().Match(s => OntologyGraph.Classes.Contains(s), "superclasses must have class definitions");
            }
        }

        [TestMethod]
        public void Classes_are_ordered()
        {
            Regex.Matches(ontologyString, @"^:\p{Lu}\w*", RegexOptions.Multiline).Select(m => m.Value).Should().BeInAscendingOrder("classes must be ordered alphabetically");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Comment_is_literal(Resource resource)
        {
            foreach (var comment in resource.Comments)
            {
                comment.NodeType.Should().Be(NodeType.Literal, "commentss must be literals");
            }
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Comment_is_not_empty(Resource resource)
        {
            foreach (var comment in resource.Comments.LiteralNodes())
            {
                comment.Value.Should().NotBeEmpty("comments must not be empty");
            }
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Comment_is_string(Resource resource)
        {
            foreach (var comment in resource.Comments.LiteralNodes().Where(comment => comment.DataType is object))
            {
                comment.DataType.AbsoluteUri.Should().BeOneOf(RdfSpecsHelper.RdfLangString, XmlSpecsHelper.XmlSchemaDataTypeString, "commentss must be strings");
            }
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Label_is_literal(Resource resource)
        {
            foreach (var label in resource.Labels)
            {
                label.NodeType.Should().Be(NodeType.Literal, "labels must be literals");
            }
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Label_is_not_empty(Resource resource)
        {
            foreach (var label in resource.Labels.LiteralNodes())
            {
                label.Value.Should().NotBeEmpty("labels must not be empty");
            }
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Label_is_string(Resource resource)
        {
            foreach (var label in resource.Labels.LiteralNodes().Where(label => label.DataType is object))
            {
                label.DataType.AbsoluteUri.Should().BeOneOf(RdfSpecsHelper.RdfLangString, XmlSpecsHelper.XmlSchemaDataTypeString, "labels must be strings");
            }
        }

        [TestMethod]
        [DynamicData(nameof(ObjectProperties))]
        public void Object_property_ranges_are_classes(Property property)
        {
            foreach (var range in property.Ranges.Where(Is_from_namespace))
            {
                range.Should().Match(r => OntologyGraph.Classes.Contains(r), "ranges must have class definitions");
            }
        }

        [TestMethod]
        public void Only_one_ontology()
        {
            Ontologies.Should().ContainSingle("graph must define exactly one ontology");
        }

        [TestMethod]
        [DynamicData(nameof(Ontologies))]
        public void Ontology_has_correct_uri(Resource ontology)
        {
            ontology.Should().Be(Vocabulary.Ontology, "ontology must have correct URI");
        }

        [TestMethod]
        public void Properties_are_ordered()
        {
            Regex.Matches(ontologyString, @"^:\p{Ll}\w*", RegexOptions.Multiline).Select(m => m.Value).Should().BeInAscendingOrder("properties must be ordered alphabetically");
        }

        [TestMethod]
        [DynamicData(nameof(Properties))]
        public void Property_domains_are_classes(Property property)
        {
            foreach (var domain in property.Domains.Where(Is_from_namespace))
            {
                domain.Should().Match(d => OntologyGraph.Classes.Contains(d), "domains must have class definitions");
            }
        }

        [TestMethod]
        [DynamicData(nameof(Properties))]
        public void Property_has_one_domain(Property property)
        {
            property.Domains.Should().ContainSingle("properties must have exactly one domain");
        }

        [TestMethod]
        [DynamicData(nameof(Properties))]
        public void Property_has_one_range(Property property)
        {
            property.Ranges.Should().ContainSingle("properties must have exactly one range");
        }

        [TestMethod]
        [DynamicData(nameof(Properties))]
        public void Property_name_is_in_camel_case(Property property)
        {
            Vocabulary.BaseUri.MakeRelativeUri(property.Uri).ToString().Should().MatchRegex(@"^[a-z]([a-z]|[A-Z]|[0-9])*$", "properties must be camelCased");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_has_one_comment(Resource resource)
        {
            resource.Comments.Should().ContainSingle("resources must have exactly one comment");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_has_one_label(Resource resource)
        {
            resource.Labels.Should().ContainSingle("resources must have exactly one label");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_has_one_type(Resource resource)
        {
            resource.Types.Should().ContainSingle("resources must have exactly one type");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_is_defined_by_correct_ontology(Resource resource)
        {
            CollectionAssert.AreEqual(OntologyGraph.Ontologies.ToList(), resource.IsDefinedBy.ToList(), "resources must be defined by correct ontology");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_is_defined_by_one_ontology(Resource resource)
        {
            resource.IsDefinedBy.Should().ContainSingle("resources must be defined by exactly one ontology");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_is_from_namespace(Resource resource)
        {
            resource.Should().Match<Resource>(r => Is_from_namespace(r), "resources must be from correct namespace");
        }

        private static bool Is_from_namespace(Resource resource)
        {
            return Vocabulary.BaseUri.IsBaseOf(resource.Uri) && !Vocabulary.BaseUri.MakeRelativeUri(resource.Uri).ToString().Contains("/", StringComparison.Ordinal);
        }
    }
}
