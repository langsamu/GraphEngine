// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class RuntimeVariablesTests
    {
        [TestMethod]
        public void Default()
        {
            var expected =
                LinqExpression.RuntimeVariables(
                    LinqExpression.Parameter(
                        typeof(object)));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    :runtimeVariablesVariables (
        [
            :parameterType [
                :typeName ""System.Object"" ;
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

