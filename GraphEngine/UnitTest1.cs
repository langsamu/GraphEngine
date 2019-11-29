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
        // http://www.easyrdf.org/converter?out=svg&raw=1&uri=x&data=@prefix+:%3Curn:%3E.:f+a+:Variable;:type[:name+%22System.Func`2%22;:typeArguments(%22System.Int64%22+%22System.Int64%22)].:n+a+:Parameter;:type+%22System.Int64%22.:s+a+:Block;:variables(:f);:expressions([a+:Assign;:left+:f;:right[a+:Lambda;:body[a+:Condition;:test[a+:LessThan;:left+:n;:right[a+:Constant;:value+2]];:ifTrue+:n;:ifFalse[a+:Add;:left[a+:Invoke;:expression+:f;:arguments([a+:Subtract;:left+:n;:right[a+:Constant;:value+2]])];:right[a+:Invoke;:expression+:f;:arguments([a+:Subtract;:left+:n;:right[a+:Constant;:value+1]])]]];:parameters(:n)]][a+:Invoke;:expression+:f;:arguments([a+:Constant;:value+8])]).
        [TestMethod]
        public void TestMethod0()
        {
            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:fib 
    a :Variable ;
    :type [
        :name ""System.Func`2"" ;
        :typeArguments (
            ""System.Int64""
            ""System.Int64""
        ) ;
    ] ;
.

:n 
    a :Parameter ;
    :type ""System.Int64"" ;
.

:s
    a :Block ;
    :variables (
        :fib
    ) ;
    :expressions (
        [
            a :Assign ;
            :left :fib ;
            :right [
                a :Lambda ;
                :body [
                    a :Condition ;
                    :test [
                        a :LessThan ;
                        :left :n ;
                        :right [
                            a :Constant ;
                            :value 2 ;
                        ] ;
                    ] ;
                    :ifTrue :n ;
                    :ifFalse [
                        a :Add ;
                        :left [
                            a :Invoke ;
                            :expression :fib ;
                            :arguments (
                                [
                                    a :Subtract ;
                                    :left :n ;
                                    :right [
                                        a :Constant ;
                                        :value 2 ;
                                    ] ;
                                ]
                            ) ;
                        ] ;
                        :right [
                            a :Invoke ;
                            :expression :fib ;
                            :arguments (
                                [
                                    a :Subtract ;
                                    :left :n ;
                                    :right [
                                        a :Constant ;
                                        :value 1 ;
                                    ] ;
                                ]
                            ) ;
                        ] ;
                    ] ;
                ] ;
                :parameters (
                    :n
                ) ;
            ] ;
        ]
        [
            a :Invoke ;
            :expression :fib ;
            :arguments (
                [
                    a :Constant ;
                    :value 8 ;
                ]
            ) ;
        ]
    ) ;
.
");

            var s = g.GetUriNode(":s");

            var expression = ExpressionNode.Parse(s).Expression;
            Console.WriteLine(GetDebugView(expression));

            var lambda = Expression.Lambda(expression);
            var actual = lambda.Compile().DynamicInvoke();

            Assert.AreEqual(21L, actual);

        }

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

            Console.WriteLine(GetDebugView(result));
        }

        public static string GetDebugView(Expression exp) => typeof(Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(exp) as string;
    }
}
