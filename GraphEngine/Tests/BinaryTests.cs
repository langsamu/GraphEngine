namespace GraphEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;

    [TestClass]
    public class BinaryTests
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[] { ExpressionType.Add };
                yield return new object[] { ExpressionType.AddAssign };
                yield return new object[] { ExpressionType.AddAssignChecked };
                yield return new object[] { ExpressionType.AddChecked };
                yield return new object[] { ExpressionType.And };
                yield return new object[] { ExpressionType.AndAlso, typeof(bool) };
                yield return new object[] { ExpressionType.AndAssign };
                yield return new object[] { ExpressionType.ArrayIndex, typeof(object[]), typeof(int) };
                yield return new object[] { ExpressionType.Assign };
                yield return new object[] { ExpressionType.Coalesce, typeof(object) };
                yield return new object[] { ExpressionType.Divide };
                yield return new object[] { ExpressionType.DivideAssign };
                yield return new object[] { ExpressionType.Equal };
                yield return new object[] { ExpressionType.ExclusiveOr };
                yield return new object[] { ExpressionType.ExclusiveOrAssign };
                yield return new object[] { ExpressionType.GreaterThan };
                yield return new object[] { ExpressionType.GreaterThanOrEqual };
                yield return new object[] { ExpressionType.LeftShift };
                yield return new object[] { ExpressionType.LeftShiftAssign };
                yield return new object[] { ExpressionType.LessThan };
                yield return new object[] { ExpressionType.LessThanOrEqual };
                yield return new object[] { ExpressionType.Modulo };
                yield return new object[] { ExpressionType.ModuloAssign };
                yield return new object[] { ExpressionType.Multiply };
                yield return new object[] { ExpressionType.MultiplyAssign };
                yield return new object[] { ExpressionType.MultiplyAssignChecked };
                yield return new object[] { ExpressionType.MultiplyChecked };
                yield return new object[] { ExpressionType.NotEqual };
                yield return new object[] { ExpressionType.Or };
                yield return new object[] { ExpressionType.OrAssign };
                yield return new object[] { ExpressionType.OrElse, typeof(bool) };
                yield return new object[] { ExpressionType.Power, typeof(double) };
                yield return new object[] { ExpressionType.PowerAssign, typeof(double) };
                yield return new object[] { ExpressionType.RightShift };
                yield return new object[] { ExpressionType.RightShiftAssign };
                yield return new object[] { ExpressionType.Subtract };
                yield return new object[] { ExpressionType.SubtractAssign };
                yield return new object[] { ExpressionType.SubtractAssignChecked };
                yield return new object[] { ExpressionType.SubtractChecked };
            }
        }

        [TestMethod]
        [DynamicData(nameof(Data))]
        public void TestMethod1(ExpressionType binaryType, Type leftType = null, Type rightType = null)
        {
            leftType ??= typeof(int);
            rightType ??= leftType;

            var expected = Expression.MakeBinary(binaryType, Expression.Parameter(leftType), Expression.Parameter(rightType));

            using var g = new Graph();
            g.LoadFromString($@"
@prefix : <http://example.com/> .

:s
    a :{binaryType} ;
    :left [
        a :Parameter ;
        :type ""{leftType}"" ;
    ] ;
    :right [
        a :Parameter ;
        :type ""{rightType}"" ;
    ] ;
.
");

            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }
    }
}
