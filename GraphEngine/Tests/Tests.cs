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
    :expressions (
        [
            a :Subtract ;
            :left [
                a :Add ;
                :left [
                    a :Constant ;
                    :value 1;
                ] ;
                :right [
                    a :Constant ;
                    :value 2;
                ] ;
            ] ;
            :right [
                a :Constant ;
                :value 3;
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
    :body [
        a :Add ;
        :left [
            a :Constant ;
            :value 1;
        ] ;
        :right [
            a :Constant ;
            :value 2;
        ] ;
    ]
.
");

            var s = g.GetUriNode(":s");
            var result = ExpressionNode.Parse(s).Expression as LambdaExpression;

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
    :type ""System.Text.StringBuilder"" ;
    :arguments (
        [
            a :Constant ;
            :value ""1""^^xsd:int;
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
    :type ""System.Text.StringBuilder"" ;
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
    :type ""System.Text.StringBuilder"" ;
    :arguments () ;
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
    :left [
        a :Variable ;
        :type ""System.Int64"" ;
    ] ;
    :right [
        a :Constant ;
        :value 0;
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
                        typeof(void)
                    ),
                    label
                )
            );


            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

_:value
    a :Parameter ;
    :type ""System.Int32"" ;
.

_:result
    a :Parameter ;
    :type ""System.Int32"" ;
.

_:label
    a :Label ;
    :type ""System.Int32"" ;
.

_:one
    a :Constant ;
    :value ""1""^^xsd:int ;
.

:s
    a :Block ;
    :variables (
        _:result
    ) ;
    :expressions (
        [
            a :Assign ;
            :left _:result ;
            :right _:one ;
        ]
        [
            a :Loop ;
            :body [
                a :Condition ;
                :test [
                    a :GreaterThan ;
                    :left _:value ;
                    :right _:one ;
                ] ;
                :ifTrue [
                    a :MultiplyAssign ;
                    :left _:result ;
                    :right [
                        a :PostDecrementAssign ;
                        :operand _:value ;
                    ] ;
                ] ;
                :ifFalse [
                    a :Break ;
                    :target _:label ;
                    :value _:result ;
                ] ;
                :type ""System.Void"" ;
            ] ;
            :break _:label ;
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
    :type ""System.Byte"" ;
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
    :type ""System.Int64"" ;
    :bounds (
        [
            a :Constant ;
            :value 0 ;
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
