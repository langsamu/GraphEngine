namespace GraphEngine.Tests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;

    [TestClass]
    public class ExampleTests
    {
        [TestMethod]
        public void FibonacciXml()
        {
            using var g = new Graph();
            g.LoadFromEmbeddedResource("GraphEngine.Resources.Examples.FibonacciSequence.xml,GraphEngine");

            NewMethod(g);
        }

        [TestMethod]
        public void FibonacciTurtle()
        {
            using var g = new Graph();
            g.LoadFromEmbeddedResource("GraphEngine.Resources.Examples.FibonacciSequence.ttl,GraphEngine");

            NewMethod(g);
        }

        [TestMethod]
        public void FibonacciJson()
        {
            using var ts = new TripleStore();
            ts.LoadFromEmbeddedResource("GraphEngine.Resources.Examples.FibonacciSequence.json,GraphEngine");
            var g = ts.Graphs.Single();
            g.NamespaceMap.AddNamespace(string.Empty, UriFactory.Create("http://example.com/"));

            NewMethod(g);
        }

        private static void NewMethod(IGraph g)
        {
            var s = g.GetUriNode(":s");

            var expression = ExpressionNode.Parse(s).Expression;
            Console.WriteLine(expression.GetDebugView());

            var lambda = Expression.Lambda(expression);
            var actual = lambda.Compile().DynamicInvoke();

            Assert.AreEqual(21L, actual);
        }
    }
}
