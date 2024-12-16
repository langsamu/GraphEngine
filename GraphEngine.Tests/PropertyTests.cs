// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class PropertyTests : TestBase
{
    [TestMethod]
    public void NameExpression()
    {
        var expected =
            LinqExpression.Property(
                LinqExpression.Parameter(typeof(SampleClass)),
                nameof(SampleClass.InstanceProperty));

        const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Property ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessName ""InstanceProperty"" ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void NameExpressionType()
    {
        var expected =
            LinqExpression.Property(
                LinqExpression.Parameter(typeof(SampleDerivedClass)),
                typeof(SampleClass),
                nameof(SampleClass.InstanceProperty));

        const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Property ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.SampleDerivedClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessType [
        :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
    ] ;
    :memberAccessName ""InstanceProperty"" ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void NameType()
    {
        var expected =
            LinqExpression.Property(
                null,
                typeof(SampleClass),
                nameof(SampleClass.StaticProperty));

        const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Property ;
    :memberAccessType [
        :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
    ] ;
    :memberAccessName ""StaticProperty"" ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void NameExpressionArguments()
    {
        var expected =
            LinqExpression.Property(
                LinqExpression.Parameter(typeof(SampleClass)),
                "Indexer",
                LinqExpression.Parameter(typeof(int)));

        const string actual = @"
@prefix : <http://example.com/> .

:s
    a :Property ;
    :memberAccessExpression [
        :parameterType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
    ] ;
    :memberAccessName ""Indexer"" ;
    :propertyArguments (
        [
            :parameterType [
                :typeName ""System.Int32"" ;
            ] ;
        ]
    ) ;
.
";

        ShouldBe(actual, expected);
    }
}
