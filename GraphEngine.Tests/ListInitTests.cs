// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class ListInitTests : TestBase
{
    [TestMethod]
    public void Default()
    {
        var expected =
            LinqExpression.ListInit(
                LinqExpression.New(
                    typeof(List<long>)),
                LinqExpression.ElementInit(
                    typeof(List<long>).GetMethod("Add"),
                    LinqExpression.Constant(0L)));

        var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :listInitNewExpression [
        :newType [
            :typeName ""System.Collections.Generic.List`1[System.Int64]"" ;
        ] ;
    ] ;
    :listInitInitializers (
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
.
";

        ShouldBe(actual, expected);
    }
}
