// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class RuntimeVariablesTests : TestBase
{
    [TestMethod]
    public void Default()
    {
        var expected =
            LinqExpression.RuntimeVariables(
                LinqExpression.Parameter(
                    typeof(object)));

        const string actual = @"
@prefix : <http://example.com/> .

:s
    :runtimeVariablesVariables (
        [
            :parameterType [
                :typeName ""System.Object"" ;
            ] ;
        ]
    ) ;
.
";

        ShouldBe(actual, expected);
    }
}
