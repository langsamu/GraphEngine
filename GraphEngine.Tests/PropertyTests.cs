// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class PropertyTests
    {
        [TestMethod]
        public void NameExpression()
        {
            var expected =
                LinqExpression.Property(
                    LinqExpression.Parameter(typeof(Cx1)),
                    nameof(Cx1.P1));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Property ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.Cx1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessName ""P1"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void NameExpressionType()
        {
            var expected =
                LinqExpression.Property(
                    LinqExpression.Parameter(typeof(Cx2)),
                    typeof(Cx1),
                    nameof(Cx1.P1));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Property ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.Cx2, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessType [
        :typeName ""GraphEngine.Tests.Cx1, GraphEngine.Tests"" ;
    ] ;
    :memberAccessName ""P1"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void NameType()
        {
            var expected =
                LinqExpression.Property(
                    null,
                    typeof(Cx1),
                    nameof(Cx1.P2));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Property ;
    :memberAccessType [
        :typeName ""GraphEngine.Tests.Cx1, GraphEngine.Tests"" ;
    ] ;
    :memberAccessName ""P2"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void NameExpressionArguments()
        {
            var expected =
                LinqExpression.Property(
                    LinqExpression.Parameter(typeof(Cx1)),
                    "Pi",
                    LinqExpression.Parameter(typeof(int)));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Property ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.Cx1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessName ""Pi"" ;
    :propertyArguments (
        [
            :parameterType [
                :typeName ""System.Int32"" ;
            ] ;
        ]
    ) ;
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