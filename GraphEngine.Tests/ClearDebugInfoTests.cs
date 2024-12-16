// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class ClearDebugInfoTests : TestBase
{
    [TestMethod]
    public void Default()
    {
        var expected =
            LinqExpression.ClearDebugInfo(
                LinqExpression.SymbolDocument(
                    string.Empty));

        const string actual = @"
@prefix : <http://example.com/> .

:s
    a :ClearDebugInfo ;
    :debugInfoDocument [
        :symbolDocumentFileName """" ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }
}
