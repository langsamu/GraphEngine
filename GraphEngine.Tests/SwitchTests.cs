// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using System.Reflection;
using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class SwitchTests
{
    [TestMethod]
    public void All()
    {
        var expected =
            LinqExpression.Switch(
                typeof(SampleClass),
                LinqExpression.Constant(0L),
                LinqExpression.Default(typeof(SampleDerivedClass)),
                typeof(SampleClass).GetMethod("Equal"),
                LinqExpression.SwitchCase(
                    LinqExpression.Default(typeof(SampleDerivedClass)),
                    LinqExpression.Constant(0L)));

        using var g = new GraphEngine.Graph();
        g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :switchType [
        :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
    ] ;
    :switchSwitchValue _:zero ;
    :switchDefaultBody [
        :defaultType [
            :typeName ""GraphEngine.Tests.SampleDerivedClass, GraphEngine.Tests"" ;
        ]
    ] ;
    :switchCases (
        [
            :caseTestValues (
                _:zero
            ) ;
            :caseBody [
                :defaultType [
                    :typeName ""GraphEngine.Tests.SampleDerivedClass, GraphEngine.Tests"" ;
                ]
            ] ;
        ]
    ) ;
    :switchComparison [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""Equal"" ;
    ] ;
.

_:zero
    :constantValue 0 ;
.
");
        var s = g.GetUriNode(":s").In(g);

        var actual = Expression.Parse(s).LinqExpression;

        actual.Should().Be(expected);

        Console.WriteLine(actual.GetDebugView());

        // Make sure custom type is used
        Assert.AreEqual(typeof(SampleClass), actual.Type);

        // Make sure custom comparison is used
        Assert.ThrowsException<TargetInvocationException>(() => LinqExpression.Lambda(actual).Compile().DynamicInvoke());
    }
}
