// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class SwitchTests
    {
        [TestMethod]
        public void All()
        {
            var expected =
                LinqExpression.Switch(
                    typeof(C1),
                    LinqExpression.Constant(0L),
                    LinqExpression.Default(typeof(C1.C2)),
                    typeof(C1).GetMethod("Equal"),
                    LinqExpression.SwitchCase(
                        LinqExpression.Default(typeof(C1.C2)),
                        LinqExpression.Constant(0L)));

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Switch ;
    :switchType [
        a :Type ;
        :typeName ""GraphEngine.Tests.SwitchTests+C1, GraphEngine.Tests"" ;
    ] ;
    :switchSwitchValue _:zero ;
    :switchDefaultBody [
        a :Default ;
        :defaultType [
            a :Type ;
            :typeName ""GraphEngine.Tests.SwitchTests+C1+C2, GraphEngine.Tests"" ;
        ]
    ] ;
    :switchCases (
        [
            a :Case ;
            :caseTestValues (
                _:zero
            ) ;
            :caseBody [
                a :Default ;
                :defaultType [
                    a :Type ;
                    :typeName ""GraphEngine.Tests.SwitchTests+C1+C2, GraphEngine.Tests"" ;
                ]
            ] ;
        ]
    ) ;
    :switchComparison [
        a :Method ;
        :memberType [
            a :Type ;
            :typeName ""GraphEngine.Tests.SwitchTests+C1, GraphEngine.Tests"" ;
        ] ;
        :memberName ""Equal"" ;
    ] ;
.

_:zero
    a :Constant ;
    :constantValue 0 ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());

            Console.WriteLine(actual.GetDebugView());

            // Make sure custom type is used
            Assert.AreEqual(typeof(C1), actual.Type);

            // Make sure custom comparison is used
            Assert.ThrowsException<TargetInvocationException>(() => LinqExpression.Lambda(actual).Compile().DynamicInvoke());
        }

        class C1
        {
            internal class C2 : C1 { }

            public static bool Equal(long obj1, long obj2) => throw new NotImplementedException();
        }
    }
}