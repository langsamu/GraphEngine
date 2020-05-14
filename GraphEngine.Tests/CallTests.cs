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

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
        :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""M1"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Static_types_no_arguments()
        {
            var expected = LinqExpression.Call(typeof(C1), nameof(C1.M2), new[] { typeof(object) }, Array.Empty<LinqExpression>());

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
        :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""M2"" ;
    :callTypeArguments (
        [
            :typeName ""System.Object""
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Static_no_types_arguments()
        {
            var expected = LinqExpression.Call(typeof(C1), nameof(C1.M3), Array.Empty<Type>(), new[] { LinqExpression.Constant(0L, typeof(long)) });

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
        :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""M3"" ;
    :callArguments (
        [
            :constantType [
                :typeName ""System.Int64"" ;
            ] ;
            :constantValue 0;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Static_types_arguments()
        {
            var expected = LinqExpression.Call(typeof(C1), nameof(C1.M4), new[] { typeof(object) }, new[] { LinqExpression.Constant(0L, typeof(long)) });

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
        :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""M4"" ;
    :callTypeArguments (
        [
            :typeName ""System.Object"" ;
        ]
    ) ;
    :callArguments (
        [
            :constantType [
                :typeName ""System.Int64"" ;
            ] ;
            :constantValue 0;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Instance_no_types_no_arguments()
        {
            var expected = LinqExpression.Call(LinqExpression.New(typeof(C1)), nameof(C1.M5), Array.Empty<Type>(), Array.Empty<LinqExpression>());

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""M5"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Instance_types_no_arguments()
        {
            var expected = LinqExpression.Call(LinqExpression.New(typeof(C1)), nameof(C1.M6), new[] { typeof(object) }, Array.Empty<LinqExpression>());

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""M6"" ;
    :callTypeArguments (
        [
            :typeName ""System.Object"" ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Instance_no_types_arguments()
        {
            var expected = LinqExpression.Call(LinqExpression.New(typeof(C1)), nameof(C1.M7), Array.Empty<Type>(), new[] { LinqExpression.Constant(0L, typeof(long)) });

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""M7"" ;
    :callArguments (
        [
            :constantType [
                :typeName ""System.Int64"" ;
            ] ;
            :constantValue 0;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Instance_types_arguments()
        {
            var expected = LinqExpression.Call(LinqExpression.New(typeof(C1)), nameof(C1.M8), new[] { typeof(object) }, new[] { LinqExpression.Constant(0L, typeof(long)) });

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""M8"" ;
    :callTypeArguments (
        [
            :typeName ""System.Object"" ;
        ]
    ) ;
    :callArguments (
        [
            :constantType [
                :typeName ""System.Int64"" ;
            ] ;
            :constantValue 0;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }
    }

    public class C1
    {
        public static void M1() { }

        public static void M2<T>() { }

        public static void M3(long arg) { }

        public static void M4<T>(long arg) { }

        public void M5() { }

        public void M6<T>() { }

        public void M7(long arg) { }

        public void M8<T>(long arg) { }
    }

    public class C2 : C1 { }
}