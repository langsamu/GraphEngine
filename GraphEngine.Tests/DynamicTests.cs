// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using Microsoft.CSharp.RuntimeBinder;
using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class DynamicTests : TestBase
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
                    [
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                    ]),
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
                    [
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                    ]),
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
}
