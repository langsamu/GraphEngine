// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;

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

            var result = ExpressionNode.Parse(s).Expression;

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
            var result = (LambdaExpression)ExpressionNode.Parse(s).Expression;

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

            var result = ExpressionNode.Parse(s).Expression;

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

            var result = ExpressionNode.Parse(s).Expression;

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

            var result = ExpressionNode.Parse(s).Expression;

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

            var result = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(result.GetDebugView());
        }

        [TestMethod]
        public void Factorial()
        {
            var value = Expression.Parameter(typeof(int));
            var result = Expression.Parameter(typeof(int));
            var label = Expression.Label(typeof(int));
            var expected = Expression.Block(
                new[] { result },
                Expression.Assign(
                    result,
                    Expression.Constant(1)),
                Expression.Loop(
                    Expression.Condition(
                        Expression.GreaterThan(
                            value,
                            Expression.Constant(1)),
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

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void Default()
        {
            var expected = Expression.Default(typeof(byte));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Default ;
    :defaultType ""System.Byte"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void NewArrayBounds()
        {
            var expected = Expression.NewArrayBounds(typeof(long), Expression.Constant(0L));

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

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void TryFault()
        {
            var expected = Expression.TryFault(
                Expression.Constant(0L),
                Expression.Constant(0L));

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

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void TryFinally()
        {
            var expected = Expression.TryFinally(
                Expression.Constant(0L),
                Expression.Constant(0L));

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

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void TryCatchTypeBody()
        {
            var expected = Expression.TryCatch(
                Expression.Constant(0L),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0L)));

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

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void TryCatchVariableBody()
        {
            var expected = Expression.TryCatch(
                Expression.Constant(0L),
                Expression.Catch(
                    Expression.Parameter(
                        typeof(Exception)),
                    Expression.Constant(0L)));

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

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void TryCatchTypeBodyFilter()
        {
            var expected = Expression.TryCatch(
                Expression.Constant(0L),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0L),
                    Expression.Equal(
                        Expression.Constant(0L),
                        Expression.Constant(0L))));

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

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void TryCatchVariableBodyFilter()
        {
            var expected = Expression.TryCatch(
                Expression.Constant(0L),
                Expression.Catch(
                    Expression.Parameter(
                        typeof(Exception)),
                    Expression.Constant(0L),
                    Expression.Equal(
                        Expression.Constant(0L),
                        Expression.Constant(0L))));

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

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }

        [TestMethod]
        public void ArrayAccess()
        {
            var expected = Expression.ArrayAccess(
                Expression.Parameter(
                    typeof(int[])),
                Expression.Parameter(
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

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }
    }
}
