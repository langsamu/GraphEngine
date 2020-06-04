// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class BlockTests : TestBase
    {
        [TestMethod]
        public void Expressions()
        {
            var expected =
                LinqExpression.Block(
                    LinqExpression.Default(typeof(string)));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :blockExpressions (
        [
            :defaultType [
                :typeName ""System.String"" ;
            ] ;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void ExpressionsVariables()
        {
            var expected =
                LinqExpression.Block(
                    new[]
                    {
                        LinqExpression.Parameter(
                            typeof(string)),
                    },
                    new[]
                    {
                        LinqExpression.Default(
                            typeof(string)),
                    });

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :blockVariables (
        [
            :parameterType [
                :typeName ""System.String"" ;
            ] ;
        ]
    ) ;
    :blockExpressions (
        [
            :defaultType [
                :typeName ""System.String"" ;
            ] ;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void TypeExpressions()
        {
            var expected =
                LinqExpression.Block(
                    typeof(object),
                    LinqExpression.Default(typeof(string)));

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :blockType [
        :typeName ""System.Object"" ;
    ] ;
    :blockExpressions (
        [
            :defaultType [
                :typeName ""System.String"" ;
            ] ;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void TypeExpressionsVariables()
        {
            var expected =
                LinqExpression.Block(
                    typeof(object),
                    new[]
                    {
                        LinqExpression.Parameter(
                            typeof(string)),
                    },
                    new[]
                    {
                        LinqExpression.Default(
                            typeof(string)),
                    });

            var actual = @"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :blockType [
        :typeName ""System.Object"" ;
    ] ;
    :blockVariables (
        [
            :parameterType [
                :typeName ""System.String"" ;
            ] ;
        ]
    ) ;
    :blockExpressions (
        [
            :defaultType [
                :typeName ""System.String"" ;
            ] ;
        ]
    ) ;
.
";

            ShouldBe(actual, expected);
        }
    }
}
