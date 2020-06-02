// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class MemberInitTests : TestBase
    {
        [TestMethod]
        public void No_bindings()
        {
            var expected =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(SampleClass)));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :memberInitNewExpression [
        :newType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Bind()
        {
            var expected =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    LinqExpression.Bind(
                        typeof(SampleClass).GetField(nameof(SampleClass.InstanceField)),
                        LinqExpression.Constant(string.Empty)));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :memberInitNewExpression [
        :newType _:C3 ;
    ] ;
    :memberInitBindings (
        [
            :bindMember [
                :memberType _:C3 ;
                :memberName ""InstanceField"" ;
            ] ;
            :bindExpression [
                :constantValue """" ;
            ] ;
        ]
    ) ;
.

_:C3
    :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void ListBind()
        {
            var expected =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    LinqExpression.ListBind(
                        typeof(SampleClass).GetProperty(nameof(SampleClass.ListProperty)),
                        LinqExpression.ElementInit(
                            typeof(List<long>).GetMethod("Add"),
                            LinqExpression.Constant(0L))));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :memberInitNewExpression [
        :newType _:C3 ;
    ] ;
    :memberInitBindings (
        [
            :bindMember [
                :memberType _:C3 ;
                :memberName ""ListProperty"" ;
            ] ;
            :listBindInitializers (
                [
                    :elementInitAddMethod [
                        :memberType [
                            :typeName ""System.Collections.Generic.List`1[System.Int64]"" ;
                        ] ;
                        :memberName ""Add"" ;
                    ] ;
                    :elementInitArguments (
                        [
                            :constantValue 0 ;
                        ]
                    ) ;
                ]
            ) ;
        ]
    ) ;
.

_:C3
    :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void MemberBind()
        {
            var expected =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    LinqExpression.MemberBind(
                        typeof(SampleClass).GetField(nameof(SampleClass.ComplexField)),
                        LinqExpression.Bind(
                            typeof(SampleClass).GetField(nameof(SampleClass.InstanceField)),
                            LinqExpression.Constant(
                                string.Empty))));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :memberInitNewExpression [
        :newType _:C3 ;
    ] ;
    :memberInitBindings (
        [
            :bindMember [
                a :Member ;
                :memberType _:C3 ;
                :memberName ""ComplexField"" ;
            ] ;
            :memberBindBindings (
                [
                    :bindMember [
                        :memberType _:C3 ;
                        :memberName ""InstanceField"" ;
                    ] ;
                    :bindExpression [
                        :constantValue """" ;
                    ] ;
                ]
            ) ;
        ]
    ) ;
.

_:C3
    a :Type ;
    :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
.
";

            ShouldBe(actual, expected);
        }
    }
}
