// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using LinqExpression = System.Linq.Expressions.Expression;

[TestClass]
public class BinaryTests : TestBase
{
    public static IEnumerable<object[]> Data
    {
        get
        {
            yield return [Linq.ExpressionType.Add];
            yield return [Linq.ExpressionType.AddAssign];
            yield return [Linq.ExpressionType.AddAssignChecked];
            yield return [Linq.ExpressionType.AddChecked];
            yield return [Linq.ExpressionType.And];
            yield return [Linq.ExpressionType.AndAlso, typeof(bool)];
            yield return [Linq.ExpressionType.AndAssign];
            yield return [Linq.ExpressionType.ArrayIndex, typeof(int[]), typeof(int)];
            yield return [Linq.ExpressionType.Assign];
            yield return [Linq.ExpressionType.Coalesce, typeof(object)];
            yield return [Linq.ExpressionType.Divide];
            yield return [Linq.ExpressionType.DivideAssign];
            yield return [Linq.ExpressionType.Equal];
            yield return [Linq.ExpressionType.ExclusiveOr];
            yield return [Linq.ExpressionType.ExclusiveOrAssign];
            yield return [Linq.ExpressionType.GreaterThan];
            yield return [Linq.ExpressionType.GreaterThanOrEqual];
            yield return [Linq.ExpressionType.LeftShift];
            yield return [Linq.ExpressionType.LeftShiftAssign];
            yield return [Linq.ExpressionType.LessThan];
            yield return [Linq.ExpressionType.LessThanOrEqual];
            yield return [Linq.ExpressionType.Modulo];
            yield return [Linq.ExpressionType.ModuloAssign];
            yield return [Linq.ExpressionType.Multiply];
            yield return [Linq.ExpressionType.MultiplyAssign];
            yield return [Linq.ExpressionType.MultiplyAssignChecked];
            yield return [Linq.ExpressionType.MultiplyChecked];
            yield return [Linq.ExpressionType.NotEqual];
            yield return [Linq.ExpressionType.Or];
            yield return [Linq.ExpressionType.OrAssign];
            yield return [Linq.ExpressionType.OrElse, typeof(bool)];
            yield return [Linq.ExpressionType.Power, typeof(double)];
            yield return [Linq.ExpressionType.PowerAssign, typeof(double)];
            yield return [Linq.ExpressionType.RightShift];
            yield return [Linq.ExpressionType.RightShiftAssign];
            yield return [Linq.ExpressionType.Subtract];
            yield return [Linq.ExpressionType.SubtractAssign];
            yield return [Linq.ExpressionType.SubtractAssignChecked];
            yield return [Linq.ExpressionType.SubtractChecked];
        }
    }

    [TestMethod]
    [DynamicData(nameof(Data))]
    public void Regular(Linq.ExpressionType binaryType, System.Type? leftType = null, System.Type? rightType = null)
    {
        leftType ??= typeof(int);
        rightType ??= leftType;

        var expected = LinqExpression.MakeBinary(
            binaryType,
            LinqExpression.Parameter(
                leftType),
            LinqExpression.Parameter(
                rightType));

        var actual = $@"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :binaryExpressionType xt:{binaryType} ;
    :binaryLeft [
        :parameterType [
            :typeName ""{leftType}"" ;
        ]
    ] ;
    :binaryRight [
        :parameterType [
            :typeName ""{rightType}"" ;
        ] ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void ReferenceEquals()
    {
        var expected =
            LinqExpression.ReferenceEqual(
                LinqExpression.Parameter(
                    typeof(object)),
                LinqExpression.Parameter(
                    typeof(object)));

        var actual = $@"
@prefix : <http://example.com/> .

:s
    a :ReferenceEqual ;
    :binaryLeft [
        :parameterType [
            :typeName ""System.Object"" ;
        ]
    ] ;
    :binaryRight [
        :parameterType [
            :typeName ""System.Object"" ;
        ] ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void ReferenceNotEquals()
    {
        var expected =
            LinqExpression.ReferenceNotEqual(
                LinqExpression.Parameter(
                    typeof(object)),
                LinqExpression.Parameter(
                    typeof(object)));

        var actual = $@"
@prefix : <http://example.com/> .

:s
    a :ReferenceNotEqual ;
    :binaryLeft [
        :parameterType [
            :typeName ""System.Object"" ;
        ]
    ] ;
    :binaryRight [
        :parameterType [
            :typeName ""System.Object"" ;
        ] ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void Method()
    {
        var zero =
            LinqExpression.Default(
                typeof(long));

        var expected =
            LinqExpression.Add(
                zero,
                zero,
                typeof(SampleClass).GetMethod(nameof(SampleClass.Equal)));

        var actual = $@"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :binaryExpressionType xt:Add ;
    :binaryLeft _:zero ;
    :binaryRight _:zero ;
    :binaryMethod [
        :memberType [
            :typeName ""GraphEngine.Tests.SampleClass, GraphEngine.Tests"" ;
        ] ;
        :memberName ""Equal"" ;
    ] ;
.

_:zero
    :defaultType [
        :typeName ""System.Int64"" ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void Conversion()
    {
        var @object = typeof(object);
        var @null =
            LinqExpression.Default(
                @object);

        var expected =
            LinqExpression.Coalesce(
                @null,
                @null,
                LinqExpression.Lambda(
                    @null,
                    LinqExpression.Parameter(
                        @object)));

        var actual = $@"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :binaryExpressionType xt:Coalesce ;
    :binaryLeft _:null ;
    :binaryRight _:null ;
    :binaryConversion [
        :lambdaBody _:null ;
        :lambdaParameters (
            [
                :parameterType _:object ;
            ]
        ) ;
    ] ;
.

_:object :typeName ""System.Object"" .
_:null :defaultType _:object .
";

        ShouldBe(actual, expected);
    }

    [TestMethod]
    public void LiftToNull()
    {
        var zero =
            LinqExpression.Default(
                typeof(int?));

        var expected =
            LinqExpression.LessThan(
                zero,
                zero,
                true,
                null);

        var actual = $@"
@prefix : <http://example.com/> .
@prefix xt: <http://example.com/ExpressionTypes/> .

:s
    :binaryExpressionType xt:LessThan ;
    :binaryLeft _:zero ;
    :binaryRight _:zero ;
    :binaryLiftToNull true ;
.

_:zero
    :defaultType [
        :typeName ""System.Nullable`1[System.Int32]"" ;
    ] ;
.
";

        ShouldBe(actual, expected);
    }
}
