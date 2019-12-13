﻿// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using VDS.RDF.Writing;
    using LinqExpression = System.Linq.Expressions.Expression;

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
            var param = LinqExpression.Parameter(typeof(int));
            var expression = LinqExpression.Add(param, param);

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
            var expression = LinqExpression.Parameter(typeof(object));

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
            var expression = LinqExpression.Parameter(typeof(object), "param");

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
            var expression = LinqExpression.ArrayLength(LinqExpression.Parameter(typeof(int[])));

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
            var value = LinqExpression.Parameter(typeof(int), "value");
            var result = LinqExpression.Parameter(typeof(int), "result");
            var label = LinqExpression.Label(typeof(int), "label");
            var one = LinqExpression.Constant(1);
            var expected = LinqExpression.Block(
                new[] { result },
                LinqExpression.Assign(
                    result,
                    one),
                LinqExpression.Loop(
                    LinqExpression.Condition(
                        LinqExpression.GreaterThan(
                            value,
                            one),
                        LinqExpression.MultiplyAssign(
                            result,
                            LinqExpression.PostDecrementAssign(
                                value)),
                        LinqExpression.Break(
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
            var expected = LinqExpression.Parameter(typeof(IEquatable<int>));

            using var g = new Graph();
            var s = g.CreateUriNode(UriFactory.Create("http://example.com/s"));
            new SerialisingVisitor(s).Visit(expected);

            new CompressingTurtleWriter(WriterCompressionLevel.Medium).Save(g, Console.Out);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var expected = LinqExpression.Break(
                LinqExpression.Label(
                    typeof(void)));

            using var g = new Graph();
            var s = g.CreateUriNode(UriFactory.Create("http://example.com/s"));
            new SerialisingVisitor(s).Visit(expected);

            new CompressingTurtleWriter(WriterCompressionLevel.Medium).Save(g, Console.Out);
        }

        private static void Compare(LinqExpression LinqExpression, string expectedRdf)
        {
            using var expected = new Graph();
            expected.LoadFromString(expectedRdf);

            var actual = new Graph();
            var s = actual.CreateUriNode(UriFactory.Create("http://example.com/s"));
            var visitor = new SerialisingVisitor(s);
            visitor.Visit(LinqExpression);

            var writer = new CompressingTurtleWriter(WriterCompressionLevel.Medium);
            Console.WriteLine(StringWriter.Write(expected, writer));
            Console.WriteLine("-------------------");
            Console.WriteLine(StringWriter.Write(actual, writer));

            Assert.AreEqual(expected, actual);
        }
    }
}
