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
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.StaticMethod),
                    Array.Empty<Type>(),
                    Array.Empty<LinqExpression>());

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
        :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""StaticMethod"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Static_types_no_arguments()
        {
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.GenericStaticMethod),
                    new[]
                    {
                        typeof(object),
                    },
                    Array.Empty<LinqExpression>());

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
        :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""GenericStaticMethod"" ;
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
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.StaticMethodWithArgument),
                    Array.Empty<Type>(),
                    new[]
                    {
                        LinqExpression.Constant(
                            0L,
                            typeof(long)),
                    });

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
        :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""StaticMethodWithArgument"" ;
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
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.GenericStaticMethodWithArgument),
                    new[]
                    {
                        typeof(object),
                    },
                    new[]
                    {
                        LinqExpression.Constant(
                            0L,
                            typeof(long)),
                    });

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
        :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
    ] ;
    :callMethod ""GenericStaticMethodWithArgument"" ;
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
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.InstanceMethod),
                    Array.Empty<Type>(),
                    Array.Empty<LinqExpression>());

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""InstanceMethod"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Instance_types_no_arguments()
        {
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.GenericInstanceMethod),
                    new[]
                    {
                        typeof(object),
                    },
                    Array.Empty<LinqExpression>());

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""GenericInstanceMethod"" ;
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
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.InstanceMethodWithArgument),
                    Array.Empty<Type>(),
                    new[]
                    {
                        LinqExpression.Constant(
                            0L,
                            typeof(long)),
                    });

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""InstanceMethodWithArgument"" ;
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
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.GenericInstanceMethodWithArgument),
                    new[]
                    {
                        typeof(object),
                    },
                    new[]
                    {
                        LinqExpression.Constant(
                            0L,
                            typeof(long)),
                    });

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod ""GenericInstanceMethodWithArgument"" ;
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
}
