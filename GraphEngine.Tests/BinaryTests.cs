// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class BinaryTests
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
        public void TestMethod1(Linq.ExpressionType binaryType, Type leftType = null, Type rightType = null)
        {
            leftType ??= typeof(int);
            rightType ??= leftType;

            var expected = LinqExpression.MakeBinary(binaryType, LinqExpression.Parameter(leftType), LinqExpression.Parameter(rightType));

            using var g = new Graph();
            g.LoadFromString($@"
@prefix : <http://example.com/> .

:s
    a :{binaryType} ;
    :binaryLeft [
        a :Parameter ;
        :parameterType ""{leftType}"" ;
    ] ;
    :binaryRight [
        a :Parameter ;
        :parameterType ""{rightType}"" ;
    ] ;
.
");

            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }
    }
}
