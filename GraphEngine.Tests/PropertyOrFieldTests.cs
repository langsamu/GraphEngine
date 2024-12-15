// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class PropertyOrFieldTests : TestBase
{
    [TestMethod]
    public void Name()
    {
        var expected =
            LinqExpression.PropertyOrField(
                LinqExpression.Parameter(typeof(SampleClass)),
                nameof(SampleClass.InstanceField));

        const string actual = @"
@prefix : <http://example.com/> .

:s
    a :PropertyOrField ;
    :memberAccessName ""InstanceField"" ;
.
";

        ((Action)(() => ShouldBe(actual, expected))).Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void ExpressionName()
    {
        var expected =
            LinqExpression.PropertyOrField(
                LinqExpression.Parameter(typeof(SampleClass)),
                nameof(SampleClass.InstanceField));

        const string actual = @"
@prefix : <http://example.com/> .

:s
    a :PropertyOrField ;
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
}
