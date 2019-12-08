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
        public void ArrayAccess()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ArrayIndex()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Binary()
        {
            var param = Expression.Parameter(typeof(int));
            var expression = Expression.Add(param, param);

            var rdf = @"
@prefix : <http://example.com/> .

:s
    a :Add ;
    :binaryLeft _:param ;
    :binaryRight _:param ;
.

_:param
    a :Parameter ;
    :parameterType [
        :typeName ""System.Int32"" ;
    ] ;
.
";

            Compare(expression, rdf);
        }

        [TestMethod]
        public void Parameter()
        {
            var expression = Expression.Parameter(typeof(object));

            var rdf = @"
@prefix : <http://example.com/> .

:s
    a :Parameter ;
    :parameterType [
        :typeName ""System.Object"" ;
    ] ;
.
";

            Compare(expression, rdf);
        }

        [TestMethod]
        public void Parameter_with_name()
        {
            var expression = Expression.Parameter(typeof(object), "param");

            var rdf = @"
@prefix : <http://example.com/> .

:s
    a :Parameter ;
    :parameterName ""param"" ;
    :parameterType [
        :typeName ""System.Object"" ;
    ] ;
.
";

            Compare(expression, rdf);
        }

        [TestMethod]
        public void Unary()
        {
            var expression = Expression.ArrayLength(Expression.Parameter(typeof(int[])));

            var rdf = @"
@prefix : <http://example.com/> .

:s
    a :ArrayLength ;
    :unaryOperand [
        a :Parameter ;
        :parameterType [
            :typeName ""System.Int32[]"" ;
        ] ;
    ] ;
    :unaryType [
        :typeName ""System.Int32"" ;
    ] ;
.
";

            Compare(expression, rdf);
        }

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

        [TestMethod]
        public void TestMethod2()
        {
            var expected = Expression.Parameter(typeof(IEquatable<int>));

            using var g = new Graph();
            var s = g.CreateUriNode(UriFactory.Create("http://example.com/s"));
            new SerialisingVisitor(s).Visit(expected);

            new CompressingTurtleWriter(WriterCompressionLevel.Medium).Save(g, Console.Out);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var expected = Expression.Break(
                Expression.Label(
                    typeof(void)));

            using var g = new Graph();
            var s = g.CreateUriNode(UriFactory.Create("http://example.com/s"));
            new SerialisingVisitor(s).Visit(expected);

            new CompressingTurtleWriter(WriterCompressionLevel.Medium).Save(g, Console.Out);
        }

        private static void Compare(Expression expression, string expectedRdf)
        {
            using var expected = new Graph();
            expected.LoadFromString(expectedRdf);

            var actual = new Graph();
            var s = actual.CreateUriNode(UriFactory.Create("http://example.com/s"));
            var visitor = new SerialisingVisitor(s);
            visitor.Visit(expression);

            var writer = new CompressingTurtleWriter(WriterCompressionLevel.Medium);
            Console.WriteLine(StringWriter.Write(expected, writer));
            Console.WriteLine("-------------------");
            Console.WriteLine(StringWriter.Write(actual, writer));

            Assert.AreEqual(expected, actual);
        }
    }
}
