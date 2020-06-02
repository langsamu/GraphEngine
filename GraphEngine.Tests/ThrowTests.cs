// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class ThrowTests : TestBase
    {
        [TestMethod]
        public void Default()
        {
            var expected =
                LinqExpression.Throw(null);

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Throw ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Value()
        {
            var expected =
                LinqExpression.Throw(
                    LinqExpression.New(
                        typeof(ArgumentException)));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    :throwValue [
        :newType [
            :typeName ""System.ArgumentException"";
        ] ;
    ] ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Value_type()
        {
            var expected =
                LinqExpression.Throw(
                    LinqExpression.New(
                        typeof(ArgumentException)),
                    typeof(Exception));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    :throwValue [
        :newType [
            :typeName ""System.ArgumentException"";
        ] ;
    ] ;
    :throwType [
        :typeName ""System.Exception"";
    ] ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void Type()
        {
            var expected =
                LinqExpression.Throw(
                    null,
                    typeof(Exception));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    :throwType [
        :typeName ""System.Exception"";
    ] ;
.
";

            ShouldBe(actual, expected);
        }
    }
}
