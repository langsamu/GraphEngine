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
        public void TestMethod1()
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
        public void TestMethod2()
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
        public void TestMethod3()
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
        public void TestMethod4()
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
        public void TestMethod5()
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
        public void TestMethod6()
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
    }
}
