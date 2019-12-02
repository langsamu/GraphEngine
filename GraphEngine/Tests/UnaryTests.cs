namespace GraphEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;

    [TestClass]
    public class UnaryTests
    {
        public static IEnumerable<object[]> UnTypedData
        {
            get
            {
                yield return new object[] { ExpressionType.ArrayLength, typeof(object[]) };
                yield return new object[] { ExpressionType.Decrement };
                yield return new object[] { ExpressionType.Increment };
                yield return new object[] { ExpressionType.IsFalse, typeof(bool) };
                yield return new object[] { ExpressionType.IsTrue, typeof(bool) };
                yield return new object[] { ExpressionType.Negate };
                yield return new object[] { ExpressionType.NegateChecked };
                yield return new object[] { ExpressionType.Not };
                yield return new object[] { ExpressionType.OnesComplement };
                yield return new object[] { ExpressionType.PostDecrementAssign };
                yield return new object[] { ExpressionType.PostIncrementAssign };
                yield return new object[] { ExpressionType.PreDecrementAssign };
                yield return new object[] { ExpressionType.PreIncrementAssign };
                yield return new object[] { ExpressionType.UnaryPlus };
            }
        }

        public static IEnumerable<object[]> TypedData
        {
            get
            {
                yield return new object[] { ExpressionType.Convert };
                yield return new object[] { ExpressionType.ConvertChecked };
                yield return new object[] { ExpressionType.Throw };
                yield return new object[] { ExpressionType.TypeAs };
                yield return new object[] { ExpressionType.Unbox, typeof(int) };
            }
        }

        [TestMethod]
        [DynamicData(nameof(UnTypedData))]
        public void UnTyped(ExpressionType expression, Type operandType = null)
        {
            operandType ??= typeof(int);

            var expected = Expression.MakeUnary(expression, Expression.Parameter(operandType), null);

            var rdf = $@"
@prefix : <http://example.com/> .

:s
    a :{expression} ;
    :operand [
        a :Parameter ;
        :type ""{operandType}"" ;
    ] ;
.
";

            Assert(rdf, expected);
        }

        [TestMethod]
        [DynamicData(nameof(TypedData))]
        public void Typed(ExpressionType expression, Type type = null)
        {
            var operandType = typeof(object);
            type ??= operandType;

            var expected = Expression.MakeUnary(expression, Expression.Parameter(operandType), type);

            var rdf = $@"
@prefix : <http://example.com/> .

:s
    a :{expression} ;
    :operand [
        a :Parameter ;
        :type ""{operandType}"" ;
    ] ;
    :type ""{type}"" ;
.
";

            Assert(rdf, expected);
        }

        [TestMethod]
        public void Lambda()
        {
            var expected = Expression.MakeUnary(ExpressionType.Quote, Expression.Lambda(Expression.Constant(0L)), null);

            var rdf = $@"
@prefix : <http://example.com/> .

:s
    a :Quote ;
    :operand [
        a :Lambda ;
        :body [
            a :Constant ;
            :value 0 ;
        ] ;
    ] ;
.
";

            Assert(rdf, expected);
        }

        private static void Assert(string rdf, Expression expected)
        {
            using var g = new Graph();
            g.LoadFromString(rdf);
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(actual.GetDebugView());

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }
    }
}
