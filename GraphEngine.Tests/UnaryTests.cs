// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linq = System.Linq.Expressions;
using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class UnaryTests : TestBase
{
    public static IEnumerable<object[]> UnTypedData
    {
        get
        {
            yield return new object[] { Linq.ExpressionType.ArrayLength, typeof(object[]) };
            yield return new object[] { Linq.ExpressionType.Decrement };
            yield return new object[] { Linq.ExpressionType.Increment };
            yield return new object[] { Linq.ExpressionType.IsFalse, typeof(bool) };
            yield return new object[] { Linq.ExpressionType.IsTrue, typeof(bool) };
            yield return new object[] { Linq.ExpressionType.Negate };
            yield return new object[] { Linq.ExpressionType.NegateChecked };
            yield return new object[] { Linq.ExpressionType.Not };
            yield return new object[] { Linq.ExpressionType.OnesComplement };
            yield return new object[] { Linq.ExpressionType.PostDecrementAssign };
            yield return new object[] { Linq.ExpressionType.PostIncrementAssign };
            yield return new object[] { Linq.ExpressionType.PreDecrementAssign };
            yield return new object[] { Linq.ExpressionType.PreIncrementAssign };
            yield return new object[] { Linq.ExpressionType.UnaryPlus };
        }
    }

    public static IEnumerable<object[]> TypedData
    {
        get
        {
            yield return new object[] { Linq.ExpressionType.Convert };
            yield return new object[] { Linq.ExpressionType.ConvertChecked };
            yield return new object[] { Linq.ExpressionType.TypeAs };
            yield return new object[] { Linq.ExpressionType.Unbox, typeof(int) };
        }
    }

    [TestMethod]
    [DynamicData(nameof(UnTypedData))]
    public void UnTyped(Linq.ExpressionType expression, Type operandType = null)
    {
        operandType ??= typeof(int);

        var expected = LinqExpression.MakeUnary(expression, LinqExpression.Parameter(operandType), null);

        var actual = $@"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :unaryExpressionType xt:{expression} ;
    :unaryOperand [
        :parameterType [
            :typeName ""{operandType}"" ;
        ] ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    [DynamicData(nameof(TypedData))]
    public void Typed(Linq.ExpressionType expression, Type type = null)
    {
        var operandType = typeof(object);
        type ??= operandType;

        var expected = LinqExpression.MakeUnary(expression, LinqExpression.Parameter(operandType), type);

        var actual = $@"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :unaryExpressionType xt:{expression} ;
    :unaryOperand [
        :parameterType [
            :typeName ""{operandType}"" ;
        ] ;
    ] ;
    :unaryType [
        :typeName ""{type}"" ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void Quote()
    {
        var expected = LinqExpression.MakeUnary(Linq.ExpressionType.Quote, LinqExpression.Lambda(LinqExpression.Constant(0L)), null);

        var actual = $@"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :unaryExpressionType xt:Quote ;
    :unaryOperand [
        :lambdaBody [
            :constantValue 0 ;
        ] ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void Method()
    {
        var expected =
            LinqExpression.Negate(
                LinqExpression.Default(
                    typeof(bool)),
                typeof(SampleClass).GetMethod(nameof(SampleClass.StaticFunctionWithArgument)));

        var actual = $@"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :unaryExpressionType xt:Negate ;
    :unaryOperand [
        :defaultType [
            :typeName ""System.Boolean"" ;
        ] ;
    ] ;
    :unaryMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""StaticFunctionWithArgument"" ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }
}
