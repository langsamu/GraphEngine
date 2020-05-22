// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void ExpressionName()
        {
            var expected =
                LinqExpression.Field(
                    LinqExpression.Parameter(typeof(Cx1)),
                    nameof(Cx1.F1));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Field ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.Cx1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessName ""F1"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void ExpressionTypeName()
        {
            var expected =
                LinqExpression.Field(
                    LinqExpression.Parameter(typeof(Cx2)),
                    typeof(Cx1),
                    nameof(Cx1.F1));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Field ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.Cx2, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessType [
        :typeName ""GraphEngine.Tests.Cx1, GraphEngine.Tests"" ;
    ] ;
    :memberAccessName ""F1"" ;
.
";

            ShouldBe(actual, expected);
        }


        [TestMethod]
        public void TypeName()
        {
            var expected =
                LinqExpression.Field(
                    null,
                    typeof(Cx1),
                    nameof(Cx1.F2));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Field ;
    :memberAccessType [
        :typeName ""GraphEngine.Tests.Cx1, GraphEngine.Tests"" ;
    ] ;
    :memberAccessName ""F2"" ;
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

    public class Cx1
    {
        public string F1;
        public static string F2;
    }

    public class Cx2 : Cx1
    {
    }
}