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
        public void Method()
        {
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass).GetMethod(nameof(SampleClass.StaticMethod)));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""StaticMethod"" ;
    ] ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Method_Arguments()
        {
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass).GetMethod(nameof(SampleClass.StaticMethodWithArgument)),
                    LinqExpression.Constant(0L));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""StaticMethodWithArgument"" ;
    ] ;
    :callArguments (
        [
            :constantValue 0;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Method_TypeArguments()
        {
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass).GetMethod(nameof(SampleClass.GenericStaticMethod)).MakeGenericMethod(typeof(object)));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""GenericStaticMethod"" ;
        :methodTypeArguments (
            [
                :typeName ""System.Object""
            ]
        ) ;
    ] ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Method_TypeArguments_Arguments()
        {
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass).GetMethod(nameof(SampleClass.GenericStaticMethodWithArgument)).MakeGenericMethod(typeof(object)),
                    LinqExpression.Constant(0L));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""GenericStaticMethodWithArgument"" ;
        :methodTypeArguments (
            [
                :typeName ""System.Object""
            ]
        ) ;
    ] ;
    :callArguments (
        [
            :constantValue 0;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Method_Instance()
        {
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    typeof(SampleClass).GetMethod(nameof(SampleClass.InstanceMethod)));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""InstanceMethod"" ;
    ] ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Method_Instance_Arguments()
        {
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    typeof(SampleClass).GetMethod(nameof(SampleClass.InstanceMethodWithArgument)),
                    LinqExpression.Constant(0L));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""InstanceMethodWithArgument"" ;
    ] ;
    :callArguments (
        [
            :constantValue 0;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Method_Instance_TypeArguments()
        {
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    typeof(SampleClass).GetMethod(nameof(SampleClass.GenericInstanceMethod)).MakeGenericMethod(typeof(object)));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""GenericInstanceMethod"" ;
        :methodTypeArguments (
            [
                :typeName ""System.Object""
            ]
        ) ;
    ] ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Method_Instance_TypeArguments_Arguments()
        {
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    typeof(SampleClass).GetMethod(nameof(SampleClass.GenericInstanceMethodWithArgument)).MakeGenericMethod(typeof(object)),
                    LinqExpression.Constant(0L));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""GenericInstanceMethodWithArgument"" ;
        :methodTypeArguments (
            [
                :typeName ""System.Object""
            ]
        ) ;
    ] ;
    :callArguments (
        [
            :constantValue 0;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Type()
        {
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.StaticMethod),
                    Array.Empty<Type>(),
                    Array.Empty<LinqExpression>());

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    :callMethodName ""StaticMethod"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Type_Arguments()
        {
            var expected =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.StaticMethodWithArgument),
                    Array.Empty<Type>(),
                    new[]
                    {
                        LinqExpression.Constant(0L),
                    });

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    :callMethodName ""StaticMethodWithArgument"" ;
    :callArguments (
        [
            :constantValue 0;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Type_TypeArguments()
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

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    :callMethodName ""GenericStaticMethod"" ;
    :callTypeArguments (
        [
            :typeName ""System.Object""
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Type_TypeArguments_Arguments()
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
                        LinqExpression.Constant(0L),
                    });

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    :callMethodName ""GenericStaticMethodWithArgument"" ;
    :callTypeArguments (
        [
            :typeName ""System.Object"" ;
        ]
    ) ;
    :callArguments (
        [
            :constantValue 0;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Default()
        {
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.InstanceMethod),
                    Array.Empty<Type>(),
                    Array.Empty<LinqExpression>());

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethodName ""InstanceMethod"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Default_Arguments()
        {
            var expected =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.InstanceMethodWithArgument),
                    Array.Empty<Type>(),
                    new[]
                    {
                        LinqExpression.Constant(0L),
                    });

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethodName ""InstanceMethodWithArgument"" ;
    :callArguments (
        [
            :constantValue 0;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Default_TypeArguments()
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

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethodName ""GenericInstanceMethod"" ;
    :callTypeArguments (
        [
            :typeName ""System.Object"" ;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Instance_TypeArgumentss_Arguments()
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
                        LinqExpression.Constant(0L),
                    });

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :callInstance [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :callMethodName ""GenericInstanceMethodWithArgument"" ;
    :callTypeArguments (
        [
            :typeName ""System.Object"" ;
        ]
    ) ;
    :callArguments (
        [
            :constantValue 0;
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
