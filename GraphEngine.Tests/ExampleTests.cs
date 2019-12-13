// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class ExampleTests
    {
        [TestMethod]
        public void FibonacciXml()
        {
            using var g = new Graph();
            g.LoadFromEmbeddedResource("GraphEngine.Tests.Resources.Examples.FibonacciSequence.xml,GraphEngine.Tests");

            NewMethod(g);
        }

        [TestMethod]
        public void FibonacciTurtle()
        {
            using var g = new Graph();
            g.LoadFromEmbeddedResource("GraphEngine.Tests.Resources.Examples.FibonacciSequence.ttl,GraphEngine.Tests");

            NewMethod(g);
        }

        [TestMethod]
        public void FibonacciJson()
        {
            using var ts = new TripleStore();
            ts.LoadFromEmbeddedResource("GraphEngine.Tests.Resources.Examples.FibonacciSequence.json,GraphEngine.Tests");
            var g = ts.Graphs.Single();
            g.NamespaceMap.AddNamespace(string.Empty, UriFactory.Create("http://example.com/"));

            NewMethod(g);
        }

        private static void NewMethod(IGraph g)
        {
            var s = g.GetUriNode(":s");

            var expression = Expression.Parse(s).LinqExpression;
            Console.WriteLine(expression.GetDebugView());

            var lambda = LinqExpression.Lambda(expression);
            var actual = lambda.Compile().DynamicInvoke();

            Assert.AreEqual(21L, actual);
        }
    }
}
