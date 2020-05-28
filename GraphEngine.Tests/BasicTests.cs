// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void POC()
        {
            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :blockExpressions (
        [
            a :Subtract ;
            :binaryLeft [
                a :Add ;
                :binaryLeft [
                    :constantValue 1;
                ] ;
                :binaryRight [
                    :constantValue 2;
                ] ;
            ] ;
            :binaryRight [
                :constantValue 3;
            ] ;
        ]
    ) ;
.
");

            var s = g.GetUriNode(":s");

            var result = Expression.Parse(s).LinqExpression;

            Console.WriteLine(result.GetDebugView());
        }

        [TestMethod]
        public void Lambda()
        {
            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :lambdaBody [
        a :Add ;
        :binaryLeft [
            :constantValue 1;
        ] ;
        :binaryRight [
            :constantValue 2;
        ] ;
    ]
.
");

            var s = g.GetUriNode(":s");
            var result = (Linq.LambdaExpression)Expression.Parse(s).LinqExpression;

            var a = result.Compile().DynamicInvoke();

            Console.WriteLine(a);
        }

        [TestMethod]
        public void NewWithArguments()
        {
            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :newType [
        :typeName ""System.Text.StringBuilder"" ;
    ] ;
    :newArguments (
        [
            :constantValue ""1""^^xsd:int;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var result = Expression.Parse(s).LinqExpression;

            Console.WriteLine(result.GetDebugView());
        }

        [TestMethod]
        public void NewWithoutArguments()
        {
            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :newType [
        :typeName ""System.Text.StringBuilder"" ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var result = Expression.Parse(s).LinqExpression;

            Console.WriteLine(result.GetDebugView());
        }

        [TestMethod]
        public void Assign()
        {
            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Assign ;
    :binaryLeft [
        :parameterType [
            :typeName ""System.Int64"" ;
        ] ;
    ] ;
    :binaryRight [
        :constantValue 0;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var result = Expression.Parse(s).LinqExpression;

            Console.WriteLine(result.GetDebugView());
        }

        [TestMethod]
        public void Factorial()
        {
            var value = LinqExpression.Parameter(typeof(int));
            var result = LinqExpression.Parameter(typeof(int));
            var label = LinqExpression.Label(typeof(int));
            var expected = LinqExpression.Block(
                new[] { result },
                LinqExpression.Assign(
                    result,
                    LinqExpression.Constant(1)),
                LinqExpression.Loop(
                    LinqExpression.Condition(
                        LinqExpression.GreaterThan(
                            value,
                            LinqExpression.Constant(1)),
                        LinqExpression.MultiplyAssign(
                            result,
                            LinqExpression.PostDecrementAssign(
                                value)),
                        LinqExpression.Break(
                            label,
                            result),
                        typeof(void)),
                    label));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

_:constantValue
    :parameterType [
        :typeName ""System.Int32"" ;
    ] ;
.

_:result
    :parameterType [
        :typeName ""System.Int32"" ;
    ] ;
.

_:label
    :targetType [
        :typeName ""System.Int32"" ;
    ] ;
.

_:one
    :constantValue ""1""^^xsd:int ;
.

:s
    :blockVariables (
        _:result
    ) ;
    :blockExpressions (
        [
            a :Assign ;
            :binaryLeft _:result ;
            :binaryRight _:one ;
        ]
        [
            :loopBody [
                :conditionTest [
                    a :GreaterThan ;
                    :binaryLeft _:constantValue ;
                    :binaryRight _:one ;
                ] ;
                :conditionIfTrue [
                    a :MultiplyAssign ;
                    :binaryLeft _:result ;
                    :binaryRight [
                        a :PostDecrementAssign ;
                        :unaryOperand _:constantValue ;
                    ] ;
                ] ;
                :conditionIfFalse [
                    a :Break ;
                    :gotoTarget _:label ;
                    :gotoValue _:result ;
                ] ;
                :conditionType [
                    :typeName ""System.Void"" ;
                ] ;
            ] ;
            :loopBreak _:label ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Default()
        {
            var expected = LinqExpression.Default(typeof(byte));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :defaultType [
        :typeName ""System.Byte"" ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void NewArrayBounds()
        {
            var expected =
                LinqExpression.NewArrayBounds(
                    typeof(long),
                    LinqExpression.Constant(0L));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :NewArrayBounds ;
    :newArrayType [
        :typeName ""System.Int64"" ;
    ] ;
    :newArrayExpressions (
        [
            :constantValue 0 ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void NewArrayInit()
        {
            var expected = LinqExpression.NewArrayInit(typeof(long), LinqExpression.Constant(0L));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :NewArrayInit ;
    :newArrayType [
        :typeName ""System.Int64"" ;
    ] ;
    :newArrayExpressions (
        [
            :constantValue 0 ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryFault()
        {
            var expected = LinqExpression.TryFault(
                LinqExpression.Constant(0L),
                LinqExpression.Constant(0L));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :tryBody _:zero ;
    :tryFault _:zero ;
.
_:zero
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryFinally()
        {
            var expected = LinqExpression.TryFinally(
                LinqExpression.Constant(0L),
                LinqExpression.Constant(0L));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :tryBody _:zero ;
    :tryFinally _:zero ;
.
_:zero
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryCatchTypeBody()
        {
            var expected = LinqExpression.TryCatch(
                LinqExpression.Constant(0L),
                LinqExpression.Catch(
                    typeof(Exception),
                    LinqExpression.Constant(0L)));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :tryBody _:zero ;
    :tryHandlers (
        [
            :catchType [
                :typeName ""System.Exception"" ;
            ] ;
            :catchBody _:zero ;
        ]
    ) ;
.
_:zero
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryCatchVariableBody()
        {
            var expected = LinqExpression.TryCatch(
                LinqExpression.Constant(0L),
                LinqExpression.Catch(
                    LinqExpression.Parameter(
                        typeof(Exception)),
                    LinqExpression.Constant(0L)));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :tryBody _:zero ;
    :tryHandlers (
        [
            :catchVariable [
                :parameterType [
                    :typeName ""System.Exception"" ;
                ] ;
            ] ;
            :catchBody _:zero ;
        ]
    ) ;
.
_:zero
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryCatchTypeBodyFilter()
        {
            var expected = LinqExpression.TryCatch(
                LinqExpression.Constant(0L),
                LinqExpression.Catch(
                    typeof(Exception),
                    LinqExpression.Constant(0L),
                    LinqExpression.Equal(
                        LinqExpression.Constant(0L),
                        LinqExpression.Constant(0L))));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :tryBody _:zero ;
    :tryHandlers (
        [
            :catchType [
                :typeName ""System.Exception"" ;
            ] ;
            :catchBody _:zero ;
            :catchFilter [
                a :Equal ;
                :binaryLeft _:zero ;
                :binaryRight _:zero ;
            ] ;
        ]
    ) ;
.
_:zero
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryCatchVariableBodyFilter()
        {
            var expected = LinqExpression.TryCatch(
                LinqExpression.Constant(0L),
                LinqExpression.Catch(
                    LinqExpression.Parameter(
                        typeof(Exception)),
                    LinqExpression.Constant(0L),
                    LinqExpression.Equal(
                        LinqExpression.Constant(0L),
                        LinqExpression.Constant(0L))));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :tryBody _:zero ;
    :tryHandlers (
        [
            :catchVariable [
                :parameterType [
                    :typeName ""System.Exception"" ;
                ] ;
            ] ;
            :catchBody _:zero ;
            :catchFilter [
                a :Equal ;
                :binaryLeft _:zero ;
                :binaryRight _:zero ;
            ] ;
        ]
    ) ;
.
_:zero
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ArrayAccess()
        {
            var expected = LinqExpression.ArrayAccess(
                LinqExpression.Parameter(
                    typeof(int[])),
                LinqExpression.Parameter(
                    typeof(int)));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :arrayAccessArray [
        :parameterType [
            :typeName ""System.Int32[]"" ;
        ] ;
    ] ;
    :arrayAccessIndexes (
        [
            :parameterType [
                :typeName ""System.Int32"" ;
            ] ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Empty()
        {
            var expected = LinqExpression.Empty();

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Empty ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Label()
        {
            var expected = LinqExpression.Label(
                LinqExpression.Label(
                    typeof(int),
                    "target"),
                LinqExpression.Parameter(
                    typeof(int)));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :labelTarget [
        :targetName ""target"" ;
        :targetType _:int ;
    ] ;
    :labelDefaultValue [
        :parameterType _:int ;
    ] ;
.

_:int
    :typeName ""System.Int32"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ArrayIndex_index()
        {
            var expected = LinqExpression.ArrayIndex(
                LinqExpression.Parameter(
                    typeof(int[])),
                LinqExpression.Parameter(
                    typeof(int)));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :arrayIndexArray [
        :parameterType [
            :typeName ""System.Int32[]"" ;
        ] ;
    ] ;
    :arrayIndexIndex [
        :parameterType [
            :typeName ""System.Int32"" ;
        ] ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ArrayIndex_indexes()
        {
            var expected = LinqExpression.ArrayIndex(
                LinqExpression.Parameter(
                    typeof(int[])),
                new[]
                {
                    LinqExpression.Parameter(
                        typeof(int)),
                });

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :arrayIndexArray [
        :parameterType [
            :typeName ""System.Int32[]"" ;
        ] ;
    ] ;
    :arrayIndexIndexes (
        [
            :parameterType [
                :typeName ""System.Int32"" ;
            ] ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Condition()
        {
            var param = LinqExpression.Parameter(typeof(bool));
            var expected = LinqExpression.Condition(param, param, param);

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :conditionTest _:param ;
    :conditionIfTrue _:param ;
    :conditionIfFalse _:param ;
.

_:param
    :parameterType [
        :typeName ""System.Boolean"" ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ConditionType()
        {
            var expected = LinqExpression.Condition(
                LinqExpression.Parameter(typeof(bool)),
                LinqExpression.Parameter(typeof(SampleClass)),
                LinqExpression.Parameter(typeof(SampleDerivedClass)),
                typeof(SampleClass));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :conditionTest [:parameterType [:typeName ""System.Boolean"" ;]] ;
    :conditionIfTrue [:parameterType _:C1] ;
    :conditionIfFalse [:parameterType [:typeName ""GraphEngine.Tests.SampleDerivedClass, GraphEngine.Tests"" ;]] ;
    :conditionType _:C1 ;
.

_:C1 :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" .
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void IfThen()
        {
            var param = LinqExpression.Parameter(typeof(bool));
            var expected = LinqExpression.IfThen(param, param);

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :IfThen ;
    :conditionTest _:param ;
    :conditionIfTrue _:param ;
.

_:param
    :parameterType [
        :typeName ""System.Boolean"" ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void IfThenElse()
        {
            var param = LinqExpression.Parameter(typeof(bool));
            var expected = LinqExpression.IfThenElse(param, param, param);

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :IfThenElse ;
    :conditionTest _:param ;
    :conditionIfTrue _:param ;
    :conditionIfFalse _:param ;
.

_:param
    :parameterType [
        :typeName ""System.Boolean"" ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EatYourOwnDogfood()
        {
            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    :lambdaBody [
        :callInstance _:g ;
        :callMethod ""Clear"" ;
    ] ;
    :lambdaParameters (
        _:g
    ) ;
.

_:g
    :parameterType [
        :typeName ""VDS.RDF.IGraph, dotNetRDF"" ;
    ] ;
.
");

            var s = g.GetUriNode(":s");

            var parsed = Expression.Parse(s).LinqExpression;
            var lambdaExpression = (Linq.LambdaExpression)parsed;
            var lambda = lambdaExpression.Compile();

            Assert.AreEqual(g.Triples.Count, 12); // 8 explicit, 4 implicit
            var result = lambda.DynamicInvoke(g);
            Assert.AreEqual(g.Triples.Count, 0);
        }

        [TestMethod]
        public void ReasoningPOC()
        {
            using var g = new GraphEngine.Graph();
            g.LoadFromEmbeddedResource("GraphEngine.Tests.Resources.Examples.FibonacciSequenceUntyped.ttl, GraphEngine.Tests");

            var s = g.GetUriNode(new Uri("http://example.com/s"));

            var expression = Expression.Parse(s).LinqExpression;
            Console.WriteLine(expression.GetDebugView());

            var lambda = LinqExpression.Lambda(expression);
            var actual = lambda.Compile().DynamicInvoke();

            Assert.AreEqual(21L, actual);
        }
    }
}
