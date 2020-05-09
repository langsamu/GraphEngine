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
    public class UnaryTests
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
                yield return new object[] { Linq.ExpressionType.Throw };
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

            var rdf = $@"
@prefix : <http://example.com/> .

:s
    a :{expression} ;
    :unaryOperand [
        a :Parameter ;
        :parameterType [
            a :Type ;
            :typeName ""{operandType}"" ;
        ] ;
    ] ;
.
";

            Assert(rdf, expected);
        }

        [TestMethod]
        [DynamicData(nameof(TypedData))]
        public void Typed(Linq.ExpressionType expression, Type type = null)
        {
            var operandType = typeof(object);
            type ??= operandType;

            var expected = LinqExpression.MakeUnary(expression, LinqExpression.Parameter(operandType), type);

            var rdf = $@"
@prefix : <http://example.com/> .

:s
    a :{expression} ;
    :unaryOperand [
        a :Parameter ;
        :parameterType [
            a :Type ;
            :typeName ""{operandType}"" ;
        ] ;
    ] ;
    :unaryType [
        a :Type ;
        :typeName ""{type}"" ;
    ] ;
.
";

            Assert(rdf, expected);
        }

        [TestMethod]
        public void Quote()
        {
            var expected = LinqExpression.MakeUnary(Linq.ExpressionType.Quote, LinqExpression.Lambda(LinqExpression.Constant(0L)), null);

            var rdf = $@"
@prefix : <http://example.com/> .

:s
    a :Quote ;
    :unaryOperand [
        a :Lambda ;
        :lambdaBody [
            a :Constant ;
            :constantValue 0 ;
        ] ;
    ] ;
.
";

            Assert(rdf, expected);
        }

        private static void Assert(string rdf, LinqExpression expected)
        {
            using var g = new Graph();
            g.LoadFromString(rdf);
            var s = g.GetUriNode(":s");

            var actual = Expression.Parse(s).LinqExpression;

            Console.WriteLine(actual.GetDebugView());

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected.GetDebugView(), actual.GetDebugView());
        }
    }
}
