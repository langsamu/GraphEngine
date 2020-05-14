// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class MembernitTests
    {
        [TestMethod]
        public void No_bindings()
        {
            var expected =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(C3)));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :memberInitNewExpression [
        :newType [
            :typeName ""GraphEngine.Tests.C3, GraphEngine.Tests"" ;
        ] ;
    ] ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Bind()
        {
            var expected =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(C3)),
                    LinqExpression.Bind(
                        typeof(C3).GetField("F1"),
                        LinqExpression.Constant(0L)));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
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
                :memberName ""F1"" ;
            ] ;
            :bindExpression [
                :constantValue 0 ;
            ] ;
        ]
    ) ;
.

_:C3
    :typeName ""GraphEngine.Tests.C3, GraphEngine.Tests"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ListBind()
        {
            var expected =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(C3)),
                    LinqExpression.ListBind(
                        typeof(C3).GetProperty("P1"),
                        LinqExpression.ElementInit(
                            typeof(List<long>).GetMethod("Add"),
                            LinqExpression.Constant(0L))));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
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
                :memberName ""P1"" ;
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
    :typeName ""GraphEngine.Tests.C3, GraphEngine.Tests"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void MemberBind()
        {
            var expected =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(C3)),
                    LinqExpression.MemberBind(
                        typeof(C3).GetField("F2"),
                        LinqExpression.Bind(
                            typeof(C3).GetField("F1"),
                            LinqExpression.Constant(0L))));

            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
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
                :memberName ""F2"" ;
            ] ;
            :memberBindBindings (
                [
                    :bindMember [
                        :memberType _:C3 ;
                        :memberName ""F1"" ;
                    ] ;
                    :bindExpression [
                        :constantValue 0 ;
                    ] ;
                ]
            ) ;
        ]
    ) ;
.

_:C3
    a :Type ;
    :typeName ""GraphEngine.Tests.C3, GraphEngine.Tests"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            actual.Should().Be(expected);
        }
    }

    public class C3
    {
        public long F1;
        public C3 F2;

        public List<long> P1 { get; set; }
    }
}