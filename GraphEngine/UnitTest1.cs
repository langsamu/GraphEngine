namespace GraphEngine
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;

    [TestClass]
    public class UnitTest1
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
                :left 1 ;
                :right 2 ;
            ] ;
            :right 3 ;
        ]
    ) ;
.
");

            var s = g.GetUriNode(":s");

            var result = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(GetDebugView(result));
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
        :left 1 ;
        :right 2 ;
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
        ""1""^^xsd:int
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var result = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(GetDebugView(result));
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

            Console.WriteLine(GetDebugView(result));
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

            Console.WriteLine(GetDebugView(result));
        }

        public static string GetDebugView(Expression exp) => typeof(Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(exp) as string;
    }
}
