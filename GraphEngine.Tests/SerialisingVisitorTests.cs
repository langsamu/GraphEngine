// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using VDS.RDF.Writing;

    [TestClass]
    public class SerialisingVisitorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var value = Expression.Parameter(typeof(int), "value");
            var result = Expression.Parameter(typeof(int), "result");
            var label = Expression.Label(typeof(int), "label");
            var one = Expression.Constant(1);
            var expected = Expression.Block(
                new[] { result },
                Expression.Assign(
                    result,
                    one),
                Expression.Loop(
                    Expression.Condition(
                        Expression.GreaterThan(
                            value,
                            one),
                        Expression.MultiplyAssign(
                            result,
                            Expression.PostDecrementAssign(
                                value)),
                        Expression.Break(
                            label,
                            result),
                        typeof(void)),
                    label));

            using var g = new Graph();
            var s = g.CreateUriNode(UriFactory.Create("http://example.com/s"));
            new SerialisingVisitor(s).Visit(expected);

            new CompressingTurtleWriter(WriterCompressionLevel.Medium).Save(g, Console.Out);
        }
    }
}
