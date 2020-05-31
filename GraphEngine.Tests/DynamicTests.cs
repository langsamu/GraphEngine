// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class DynamicTests
    {
        [TestMethod]
        public void InvokeMember()
        {
            var expected =
                LinqExpression.Dynamic(
                    Binder.InvokeMember(
                        CSharpBinderFlags.None,
                        "ToString",
                        null,
                        null,
                        new[]
                        {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        }),
                    typeof(object),
                    LinqExpression.Constant(0L));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    :dynamicBinder [
        a :InvokeMember ;
        :binderName ""ToString"";
        :binderArguments (
            []
        ) ;
    ] ;
    :dynamicReturnType [
        :typeName ""System.Object"" ;
    ] ;
    :dynamicArguments (
        [
            :constantValue 0 ;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void BinaryOperation()
        {
            var expected =
                LinqExpression.Dynamic(
                    Binder.BinaryOperation(
                        CSharpBinderFlags.None,
                        Linq.ExpressionType.Add,
                        null,
                        new[]
                        {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        }),
                    typeof(object),
                    LinqExpression.Constant(2L),
                    LinqExpression.Constant(3L));

            const string actual = @"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :dynamicBinder [
        a :BinaryOperation ;
        :binderExpressionType xt:Add ;
        :binderArguments (
            []
            []
        ) ;
    ] ;
    :dynamicReturnType [
        :typeName ""System.Object"" ;
    ] ;
    :dynamicArguments (
        [
            :constantValue 2 ;
        ]
        [
            :constantValue 3 ;
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
