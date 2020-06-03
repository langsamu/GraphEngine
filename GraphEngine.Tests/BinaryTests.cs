// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Linq = System.Linq.Expressions;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class BinaryTests : TestBase
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[] { Linq.ExpressionType.Add };
                yield return new object[] { Linq.ExpressionType.AddAssign };
                yield return new object[] { Linq.ExpressionType.AddAssignChecked };
                yield return new object[] { Linq.ExpressionType.AddChecked };
                yield return new object[] { Linq.ExpressionType.And };
                yield return new object[] { Linq.ExpressionType.AndAlso, typeof(bool) };
                yield return new object[] { Linq.ExpressionType.AndAssign };
                yield return new object[] { Linq.ExpressionType.ArrayIndex, typeof(int[]), typeof(int) };
                yield return new object[] { Linq.ExpressionType.Assign };
                yield return new object[] { Linq.ExpressionType.Coalesce, typeof(object) };
                yield return new object[] { Linq.ExpressionType.Divide };
                yield return new object[] { Linq.ExpressionType.DivideAssign };
                yield return new object[] { Linq.ExpressionType.Equal };
                yield return new object[] { Linq.ExpressionType.ExclusiveOr };
                yield return new object[] { Linq.ExpressionType.ExclusiveOrAssign };
                yield return new object[] { Linq.ExpressionType.GreaterThan };
                yield return new object[] { Linq.ExpressionType.GreaterThanOrEqual };
                yield return new object[] { Linq.ExpressionType.LeftShift };
                yield return new object[] { Linq.ExpressionType.LeftShiftAssign };
                yield return new object[] { Linq.ExpressionType.LessThan };
                yield return new object[] { Linq.ExpressionType.LessThanOrEqual };
                yield return new object[] { Linq.ExpressionType.Modulo };
                yield return new object[] { Linq.ExpressionType.ModuloAssign };
                yield return new object[] { Linq.ExpressionType.Multiply };
                yield return new object[] { Linq.ExpressionType.MultiplyAssign };
                yield return new object[] { Linq.ExpressionType.MultiplyAssignChecked };
                yield return new object[] { Linq.ExpressionType.MultiplyChecked };
                yield return new object[] { Linq.ExpressionType.NotEqual };
                yield return new object[] { Linq.ExpressionType.Or };
                yield return new object[] { Linq.ExpressionType.OrAssign };
                yield return new object[] { Linq.ExpressionType.OrElse, typeof(bool) };
                yield return new object[] { Linq.ExpressionType.Power, typeof(double) };
                yield return new object[] { Linq.ExpressionType.PowerAssign, typeof(double) };
                yield return new object[] { Linq.ExpressionType.RightShift };
                yield return new object[] { Linq.ExpressionType.RightShiftAssign };
                yield return new object[] { Linq.ExpressionType.Subtract };
                yield return new object[] { Linq.ExpressionType.SubtractAssign };
                yield return new object[] { Linq.ExpressionType.SubtractAssignChecked };
                yield return new object[] { Linq.ExpressionType.SubtractChecked };
            }
        }

        [TestMethod]
        [DynamicData(nameof(Data))]
        public void Regular(Linq.ExpressionType binaryType, Type leftType = null, Type rightType = null)
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
}
