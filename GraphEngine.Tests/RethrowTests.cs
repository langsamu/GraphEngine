// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class RethrowTests
    {
        [TestMethod]
        public void Default()
        {
            var expected =
                LinqExpression.Rethrow();

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Rethrow ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Type()
        {
            var expected =
                LinqExpression.Rethrow(
                    typeof(Exception));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Rethrow ;
    :throwType [
        :typeName ""System.Exception"";
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
