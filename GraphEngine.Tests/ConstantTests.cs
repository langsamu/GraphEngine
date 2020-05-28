// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class ConstantTests
    {
        [TestMethod]
        public void Null()
        {
            var expected =
                LinqExpression.Constant(
                    null);

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Constant ; # needs explicit type statement because there's no other predicate to infer from
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Null_with_type()
        {
            var expected =
                LinqExpression.Constant(
                    null,
                    typeof(string));

            const string actual = @"
@prefix : <http://example.com/> .

:s 
    :constantType [
        :typeName ""System.String"" ;
    ] ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Uri()
        {
            var expected =
                LinqExpression.Constant(
                    UriFactory.Create("urn:s"));

            const string actual = @"
@prefix : <http://example.com/> .

:s 
    :constantValue <urn:s> ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void LangString()
        {
            var expected =
                LinqExpression.Constant(
                    new NodeFactory().CreateLiteralNode(string.Empty, "en"));

            const string actual = @"
@prefix : <http://example.com/> .

:s 
    :constantValue """"@en ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Long_value()
        {
            var expected =
                LinqExpression.Constant(
                    0L);

            const string actual = @"
@prefix : <http://example.com/> .

:s 
    :constantValue 0 ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Int_value()
        {
            var expected =
                LinqExpression.Constant(
                    0);

            const string actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s 
    :constantValue ""0""^^xsd:int ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void String_value()
        {
            var expected =
                LinqExpression.Constant(
                    "x");

            const string actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s 
    :constantValue ""x"" ;
.
";

            ShouldBe(actual, expected);
        }

        private static void ShouldBe(string rdf, LinqExpression expected)
        {
            using var g = new GraphEngine.Graph();
            g.LoadFromString(rdf);
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }
    }
}
