// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class DebugInfoTests : TestBase
{
    [TestMethod]
    public void Default()
    {
        var expected =
            LinqExpression.DebugInfo(
                LinqExpression.SymbolDocument(
                    string.Empty),
                1,
                1,
                1,
                1);

        const string actual = @"
@prefix : <http://example.com/> .

:s
    :debugInfoDocument [
        :symbolDocumentFileName """" ;
    ] ;
    :debugInfoStartLine 1 ;
    :debugInfoStartColumn 1 ;
    :debugInfoEndLine 1 ;
    :debugInfoEndColumn 1 ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void Language()
    {
        var expected =
            LinqExpression.DebugInfo(
                LinqExpression.SymbolDocument(
                    string.Empty,
                    new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a5")),
                1,
                1,
                1,
                1);

        const string actual = @"
@prefix : <http://example.com/> .

:s
    :debugInfoDocument [
        :symbolDocumentFileName """" ;
        :symbolDocumentLanguage <urn:uuid:61eac4f1-bb04-4197-a7bd-eb5749f343a5> ;
    ] ;
    :debugInfoStartLine 1 ;
    :debugInfoStartColumn 1 ;
    :debugInfoEndLine 1 ;
    :debugInfoEndColumn 1 ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void LanguageVendor()
    {
        var expected =
            LinqExpression.DebugInfo(
                LinqExpression.SymbolDocument(
                    string.Empty,
                    new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a5"),
                    new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a6")),
                1,
                1,
                1,
                1);

        const string actual = @"
@prefix : <http://example.com/> .

:s
    :debugInfoDocument [
        :symbolDocumentFileName """" ;
        :symbolDocumentLanguage <urn:uuid:61eac4f1-bb04-4197-a7bd-eb5749f343a5> ;
        :symbolDocumentLanguageVendor <urn:uuid:61eac4f1-bb04-4197-a7bd-eb5749f343a6> ;
    ] ;
    :debugInfoStartLine 1 ;
    :debugInfoStartColumn 1 ;
    :debugInfoEndLine 1 ;
    :debugInfoEndColumn 1 ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void LanguageVendorType()
    {
        var expected =
            LinqExpression.DebugInfo(
                LinqExpression.SymbolDocument(
                    string.Empty,
                    new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a5"),
                    new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a6"),
                    new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a7")),
                1,
                1,
                1,
                1);

        const string actual = @"
@prefix : <http://example.com/> .

:s
    :debugInfoDocument [
        :symbolDocumentFileName """" ;
        :symbolDocumentLanguage <urn:uuid:61eac4f1-bb04-4197-a7bd-eb5749f343a5> ;
        :symbolDocumentLanguageVendor <urn:uuid:61eac4f1-bb04-4197-a7bd-eb5749f343a6> ;
        :symbolDocumentDocumentType <urn:uuid:61eac4f1-bb04-4197-a7bd-eb5749f343a7> ;
    ] ;
    :debugInfoStartLine 1 ;
    :debugInfoStartColumn 1 ;
    :debugInfoEndLine 1 ;
    :debugInfoEndColumn 1 ;
.
";

        ShouldBe(actual, expected);
    }
}
