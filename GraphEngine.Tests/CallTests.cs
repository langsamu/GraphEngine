// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class CallTests
    {
        [TestMethod]
        public void Static_no_types_no_arguments()
        {
            var expected = LinqExpression.Call(typeof(C1), nameof(C1.M1), Array.Empty<Type>(), Array.Empty<LinqExpression>());

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :callType [
        a :Type ;
        :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""M1"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Static_types_no_arguments()
        {
            var expected = LinqExpression.Call(typeof(C1), nameof(C1.M2), new[] { typeof(object) }, Array.Empty<LinqExpression>());

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :callType [
        a :Type ;
        :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""M2"" ;
    :callTypeArguments (
        [
            a :Type ;
            :typeName ""System.Object""
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Static_no_types_arguments()
        {
            var expected = LinqExpression.Call(typeof(C1), nameof(C1.M3), Array.Empty<Type>(), new[] { LinqExpression.Constant(0, typeof(object)) });

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :callType [
        a :Type ;
        :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""M3"" ;
    :callArguments (
        [
            a :Constant ;
            :constantType [
                a :Type ;
                :typeName ""System.Object"" ;
            ] ;
            :constantValue 0;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Static_types_arguments()
        {
            var expected = LinqExpression.Call(typeof(C1), nameof(C1.M4), new[] { typeof(object) }, new[] { LinqExpression.Constant(0, typeof(object)) });

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :callType [
        a :Type ;
        :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""M4"" ;
    :callTypeArguments (
        [
            a :Type ;
            :typeName ""System.Object"" ;
        ]
    ) ;
    :callArguments (
        [
            a :Constant ;
            :constantType [
                a :Type ;
                :typeName ""System.Object"" ;
            ] ;
            :constantValue 0;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Instance_no_types_no_arguments()
        {
            var expected = LinqExpression.Call(LinqExpression.New(typeof(C1)), nameof(C1.M5), Array.Empty<Type>(), Array.Empty<LinqExpression>());

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :callInstance [
        a :New ;
        :newType [
            a :Type ;
            :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""M5"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Instance_types_no_arguments()
        {
            var expected = LinqExpression.Call(LinqExpression.New(typeof(C1)), nameof(C1.M6), new[] { typeof(object) }, Array.Empty<LinqExpression>());

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :callInstance [
        a :New ;
        :newType [
            a :Type ;
            :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""M6"" ;
    :callTypeArguments (
        [
            a :Type ;
            :typeName ""System.Object"" ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Instance_no_types_arguments()
        {
            var expected = LinqExpression.Call(LinqExpression.New(typeof(C1)), nameof(C1.M7), Array.Empty<Type>(), new[] { LinqExpression.Constant(0, typeof(object)) });

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :callInstance [
        a :New ;
        :newType [
            a :Type ;
            :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""M7"" ;
    :callArguments (
        [
            a :Constant ;
            :constantType [
                a :Type ;
                :typeName ""System.Object"" ;
            ] ;
            :constantValue 0;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Instance_types_arguments()
        {
            var expected = LinqExpression.Call(LinqExpression.New(typeof(C1)), nameof(C1.M8), new[] { typeof(object) }, new[] { LinqExpression.Constant(0, typeof(object)) });

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :callInstance [
        a :New ;
        :newType [
            a :Type ;
            :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""M8"" ;
    :callTypeArguments (
        [
            a :Type ;
            :typeName ""System.Object"" ;
        ]
    ) ;
    :callArguments (
        [
            a :Constant ;
            :constantType [
                a :Type ;
                :typeName ""System.Object"" ;
            ] ;
            :constantValue 0;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }
    }

    public class C1
    {
        public static void M1() { }

        public static void M2<T>() { }

        public static void M3(object arg) { }

        public static void M4<T>(object arg) { }

        public void M5() { }

        public void M6<T>() { }

        public void M7(object arg) { }

        public void M8<T>(object arg) { }
    }

    public class C2 : C1 { }
}