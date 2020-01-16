// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void POC()
        {
            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Block ;
    :blockExpressions (
        [
            a :Subtract ;
            :binaryLeft [
                a :Add ;
                :binaryLeft [
                    a :Constant ;
                    :constantValue 1;
                ] ;
                :binaryRight [
                    a :Constant ;
                    :constantValue 2;
                ] ;
            ] ;
            :binaryRight [
                a :Constant ;
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
            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Lambda ;
    :lambdaBody [
        a :Add ;
        :binaryLeft [
            a :Constant ;
            :constantValue 1;
        ] ;
        :binaryRight [
            a :Constant ;
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
            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :New ;
    :newType ""System.Text.StringBuilder"" ;
    :newArguments (
        [
            a :Constant ;
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
            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :New ;
    :newType ""System.Text.StringBuilder"" ;
.
");
            var s = g.GetUriNode(":s");

            var result = Expression.Parse(s).LinqExpression;

            Console.WriteLine(result.GetDebugView());
        }

        [TestMethod]
        public void NewWithEmptyArguments()
        {
            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :New ;
    :newType ""System.Text.StringBuilder"" ;
    :newArguments () ;
.
");
            var s = g.GetUriNode(":s");

            var result = Expression.Parse(s).LinqExpression;

            Console.WriteLine(result.GetDebugView());
        }

        [TestMethod]
        public void Assign()
        {
            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Assign ;
    :binaryLeft [
        a :Variable ;
        :parameterType ""System.Int64"" ;
    ] ;
    :binaryRight [
        a :Constant ;
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

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

_:constantValue
    a :Parameter ;
    :parameterType ""System.Int32"" ;
.

_:result
    a :Parameter ;
    :parameterType ""System.Int32"" ;
.

_:label
    a :Label ;
    :labelType ""System.Int32"" ;
.

_:one
    a :Constant ;
    :constantValue ""1""^^xsd:int ;
.

:s
    a :Block ;
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
            a :Loop ;
            :loopBody [
                a :Condition ;
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
                :conditionType ""System.Void"" ;
            ] ;
            :loopBreak _:label ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Default()
        {
            var expected = LinqExpression.Default(typeof(byte));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Default ;
    :defaultType ""System.Byte"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void NewArrayBounds()
        {
            var expected = LinqExpression.NewArrayBounds(typeof(long), LinqExpression.Constant(0L));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :NewArrayBounds ;
    :newArrayBoundsType ""System.Int64"" ;
    :newArrayBoundsBounds (
        [
            a :Constant ;
            :constantValue 0 ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void TryFault()
        {
            var expected = LinqExpression.TryFault(
                LinqExpression.Constant(0L),
                LinqExpression.Constant(0L));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Try ;
    :tryBody _:zero ;
    :tryFault _:zero ;
.
_:zero
    a :Constant ;
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void TryFinally()
        {
            var expected = LinqExpression.TryFinally(
                LinqExpression.Constant(0L),
                LinqExpression.Constant(0L));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Try ;
    :tryBody _:zero ;
    :tryFinally _:zero ;
.
_:zero
    a :Constant ;
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void TryCatchTypeBody()
        {
            var expected = LinqExpression.TryCatch(
                LinqExpression.Constant(0L),
                LinqExpression.Catch(
                    typeof(Exception),
                    LinqExpression.Constant(0L)));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Try ;
    :tryBody _:zero ;
    :tryHandlers (
        [
            :catchType ""System.Exception"" ;
            :catchBody _:zero ;
        ]
    ) ;
.
_:zero
    a :Constant ;
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
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

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Try ;
    :tryBody _:zero ;
    :tryHandlers (
        [
            :catchVariable [
                a :Parameter ;
                :parameterType ""System.Exception"" ;
            ] ;
            :catchBody _:zero ;
        ]
    ) ;
.
_:zero
    a :Constant ;
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
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

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Try ;
    :tryBody _:zero ;
    :tryHandlers (
        [
            :catchType ""System.Exception"" ;
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
    a :Constant ;
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
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

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Try ;
    :tryBody _:zero ;
    :tryHandlers (
        [
            :catchVariable [
                a :Parameter ;
                :parameterType ""System.Exception"" ;
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
    a :Constant ;
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void ArrayAccess()
        {
            var expected = LinqExpression.ArrayAccess(
                LinqExpression.Parameter(
                    typeof(int[])),
                LinqExpression.Parameter(
                    typeof(int)));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :ArrayAccess ;
    :arrayAccessArray [
        a :Parameter ;
        :parameterType ""System.Int32[]"" ;
    ] ;
    :arrayAccessIndexes (
        [
            a :Parameter ;
            :parameterType ""System.Int32"" ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Empty()
        {
            var expected = LinqExpression.Empty();

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Empty ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
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

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Label ;
    :labelTarget [
        :targetName ""target"" ;
        :targetType _:int ;
    ] ;
    :labelDefaultValue [
        a :Parameter ;
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

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void ArrayIndex_index()
        {
            var expected = LinqExpression.ArrayIndex(
                LinqExpression.Parameter(
                    typeof(int[])),
                LinqExpression.Parameter(
                    typeof(int)));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :ArrayIndex ;
    :arrayIndexArray [
        a :Parameter ;
        :parameterType ""System.Int32[]"" ;
    ] ;
    :arrayIndexIndex [
        a :Parameter ;
        :parameterType ""System.Int32"" ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
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
                        typeof(int))
                });

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :ArrayIndex ;
    :arrayIndexArray [
        a :Parameter ;
        :parameterType ""System.Int32[]"" ;
    ] ;
    :arrayIndexIndexes (
        [
            a :Parameter ;
            :parameterType ""System.Int32"" ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Condition()
        {
            var param = LinqExpression.Parameter(typeof(bool));
            var expected = LinqExpression.Condition(param, param, param);

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Condition ;
    :conditionTest _:param ;
    :conditionIfTrue _:param ;
    :conditionIfFalse _:param ;
.

_:param
    a :Parameter ;
    :parameterType [
        :typeName ""System.Boolean"" ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void ConditionType()
        {
            var expected = LinqExpression.Condition(
                LinqExpression.Parameter(typeof(bool)),
                LinqExpression.Parameter(typeof(C1)),
                LinqExpression.Parameter(typeof(C2)),
                typeof(C1));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Condition ;
    :conditionTest [a :Parameter; :parameterType [:typeName ""System.Boolean"" ;]] ;
    :conditionIfTrue [a :Parameter; :parameterType _:C1] ;
    :conditionIfFalse [a :Parameter; :parameterType [:typeName ""GraphEngine.Tests.C2, GraphEngine.Tests"" ;]] ;
    :conditionType _:C1 ;
.

_:C1 :typeName ""GraphEngine.Tests.C1, GraphEngine.Tests"" .
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void IfThen()
        {
            var param = LinqExpression.Parameter(typeof(bool));
            var expected = LinqExpression.IfThen(param, param);

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :IfThen ;
    :conditionTest _:param ;
    :conditionIfTrue _:param ;
.

_:param
    a :Parameter ;
    :parameterType [
        :typeName ""System.Boolean"" ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void IfThenElse()
        {
            var param = LinqExpression.Parameter(typeof(bool));
            var expected = LinqExpression.IfThenElse(param, param, param);

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :IfThenElse ;
    :conditionTest _:param ;
    :conditionIfTrue _:param ;
    :conditionIfFalse _:param ;
.

_:param
    a :Parameter ;
    :parameterType [
        :typeName ""System.Boolean"" ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void EatYourOwnDogfood()
        {
            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Lambda ;
    :lambdaBody [
        a :Call ;
        :callInstance _:g ;
        :callMethod ""Clear"" ;
    ] ;
    :lambdaParameters (
        _:g
    ) ;
.

_:g
    a :Parameter ;
    :parameterType ""VDS.RDF.IGraph, dotNetRDF"" ;
.
");

            var s = g.GetUriNode(":s");

            var parsed = Expression.Parse(s).LinqExpression;
            var lambdaExpression = (Linq.LambdaExpression)parsed;
            var lambda = lambdaExpression.Compile();

            Assert.AreEqual(g.Triples.Count, 10);
            var result = lambda.DynamicInvoke(g);
            Assert.AreEqual(g.Triples.Count, 0);
        }
    }
}
