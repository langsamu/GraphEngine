// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class TypeBinaryTests : TestBase
{
    [TestMethod]
    public void TypeEqual()
    {
        var expected =
            LinqExpression.TypeEqual(
                LinqExpression.Empty(),
                typeof(object));

        const string actual = @"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :typeBinaryExpressionType xt:TypeEqual ;
    :typeBinaryExpression [
        a :Empty ;
    ];
    :typeBinaryType [
        :typeName ""System.Object"" ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void TypeIs()
    {
        var expected =
            LinqExpression.TypeIs(
                LinqExpression.Empty(),
                typeof(object));

        const string actual = @"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :typeBinaryExpressionType xt:TypeIs ;
    :typeBinaryExpression [
        a :Empty ;
    ];
    :typeBinaryType [
        :typeName ""System.Object"" ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }
}
