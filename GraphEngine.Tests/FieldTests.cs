// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class FieldTests : TestBase
    {
        [TestMethod]
        public void ExpressionName()
        {
            var expected =
                LinqExpression.Field(
                    LinqExpression.Parameter(typeof(SampleClass)),
                    nameof(SampleClass.InstanceField));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Field ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessName ""InstanceField"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void ExpressionTypeName()
        {
            var expected =
                LinqExpression.Field(
                    LinqExpression.Parameter(typeof(SampleDerivedClass)),
                    typeof(SampleClass),
                    nameof(SampleClass.InstanceField));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Field ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.SampleDerivedClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessType [
        :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
    ] ;
    :memberAccessName ""InstanceField"" ;
.
";

            ShouldBe(actual, expected);
        }

        [TestMethod]
        public void TypeName()
        {
            var expected =
                LinqExpression.Field(
                    null,
                    typeof(SampleClass),
                    nameof(SampleClass.StaticField));

            const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Field ;
    :memberAccessType [
        :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
    ] ;
    :memberAccessName ""StaticField"" ;
.
";

            ShouldBe(actual, expected);
        }
    }
}
