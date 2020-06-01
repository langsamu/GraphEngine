// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class TypeBinaryTests
    {
        [TestMethod]
        public void TypeEqual()
        {
            var expected =
                LinqExpression.TypeEqual(
                    LinqExpression.Empty(),
                    typeof(object));

            const string actual = @"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :typeBinaryExpressionType xt:TypeEqual ;
    :typeBinaryExpression [
        a :Empty ;
    ];
    :typeBinaryType [
        :typeName ""System.Object"" ;
    ] ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void TypeIs()
        {
            var expected =
                LinqExpression.TypeIs(
                    LinqExpression.Empty(),
                    typeof(object));

            const string actual = @"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :typeBinaryExpressionType xt:TypeIs ;
    :typeBinaryExpression [
        a :Empty ;
    ];
    :typeBinaryType [
        :typeName ""System.Object"" ;
    ] ;
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
