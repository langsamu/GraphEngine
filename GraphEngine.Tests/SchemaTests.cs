// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using GraphEngine.Ontology;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using VDS.RDF.Parsing;

    [TestClass]
    public class SchemaTests
    {
        private static readonly Ontology.Graph ontologyGraph = new GraphEngine.Ontology.Graph();

        static SchemaTests()
        {
            ontologyGraph.LoadFromEmbeddedResource("GraphEngine.Resources.Schema.ttl, GraphEngine");
        }

        private static IEnumerable<object[]> Ontologies => ontologyGraph.Ontologies.Select(o => new[] { o });

        private static IEnumerable<object[]> Properties => ontologyGraph.DatatypeProperties.Union(ontologyGraph.ObjectProperties).Select(p => new[] { p });

        private static IEnumerable<object[]> Classes => ontologyGraph.Classes.Select(c => new[] { c });

        private static IEnumerable<object[]> Resources => Ontologies.Union(Classes).Union(Properties);

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
        [DynamicData(nameof(Resources))]
        public void Resource_is_defined_by_one_ontology(Resource resource)
        {
            resource.IsDefinedBy.Should().ContainSingle("resources must be defined by exactly one ontology");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_is_defined_by_correct_ontology(Resource resource)
        {
            CollectionAssert.AreEqual(ontologyGraph.Ontologies.ToList(), resource.IsDefinedBy.ToList(), "resources must be defined by correct ontology");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_has_one_type(Resource resource)
        {
            resource.Types.Should().ContainSingle("resources must have exactly one type");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_is_from_namespace(Resource resource)
        {
            resource
                .Should()
                .Match<Resource>(r => Vocabulary.BaseUri.IsBaseOf(resource.Uri), "resources must be from correct namespace")
                .And
                .Match<Resource>(r => !Vocabulary.BaseUri.MakeRelativeUri(resource.Uri).ToString().Contains("/"), "properties must be camelCased");
        }

        [TestMethod]
        [DynamicData(nameof(Properties))]
        public void Properties_have_one_domain(Property property)
        {
            property.Domains.Should().ContainSingle("properties must have exactly one domain");
        }

        [TestMethod]
        [DynamicData(nameof(Properties))]
        public void Properties_have_one_range(Property property)
        {
            property.Ranges.Should().ContainSingle("properties must have exactly one range");
        }

        [TestMethod]
        [DynamicData(nameof(Properties))]
        public void Properties_are_camel_cased(Property property)
        {
            Vocabulary.BaseUri.MakeRelativeUri(property.Uri).ToString().Should().MatchRegex(@"^[a-z]([a-z]|[A-Z]|[0-9])*$", "properties must be camelCased");
        }

        [TestMethod]
        [DynamicData(nameof(Classes))]
        public void Classes_are_pascal_cased(Resource @class)
        {
            Vocabulary.BaseUri.MakeRelativeUri(@class.Uri).ToString().Should().MatchRegex(@"^[A-Z]([a-z]|[A-Z]|[0-9])*$", "classes must be PascalCased");
        }

        [TestMethod]
        [DynamicData(nameof(Resources))]
        public void Resource_has_one_label(Resource resource)
        {
            resource.Labels.Should().ContainSingle("resources must have exactly one label");
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
        [DynamicData(nameof(Resources))]
        public void Resource_has_one_comment(Resource resource)
        {
            resource.Comments.Should().ContainSingle("resources must have exactly one comment");
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
    }
}
