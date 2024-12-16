// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class RethrowTests : TestBase
{
    [TestMethod]
    public void Default()
    {
        var expected =
            LinqExpression.Rethrow();

        const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Rethrow ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void Type()
    {
        var expected =
            LinqExpression.Rethrow(
                typeof(Exception));

        const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Rethrow ;
    :throwType [
        :typeName ""System.Exception"";
    ] ;
.
";

        ShouldBe(actual, expected);
    }
}
