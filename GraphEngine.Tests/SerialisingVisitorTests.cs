// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF.Writing;
    using LinqExpression = System.Linq.Expressions.Expression;

    [TestClass]
    public class SerialisingVisitorTests
    {
        [TestMethod]
        public void ArrayAccess()
        {
            var expression =
                LinqExpression.ArrayAccess(
                    LinqExpression.Parameter(
                        typeof(int[])),
                    LinqExpression.Parameter(
                        typeof(int)));

            ShouldRoundrip(expression);
        }

        //[TestMethod]
        public void ArrayIndex()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Binary()
        {
            var param = LinqExpression.Parameter(typeof(int));
            var expression = LinqExpression.Add(param, param);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Empty()
        {
            var expression = LinqExpression.Empty();

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Block()
        {
            var expression = LinqExpression.Block(new[] { LinqExpression.Parameter(typeof(int)) }, new[] { LinqExpression.Empty() });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Condition()
        {
            var param = LinqExpression.Parameter(typeof(bool));
            var expression = LinqExpression.Condition(param, param, param);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void ConditionType()
        {
            var expression = LinqExpression.Condition(
                LinqExpression.Parameter(typeof(bool)),
                LinqExpression.Parameter(typeof(C2)),
                LinqExpression.Parameter(typeof(C1)),
                typeof(C1));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void IfThen()
        {
            var param = LinqExpression.Parameter(typeof(bool));
            var expression = LinqExpression.IfThen(param, param);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void IfThenElse()
        {
            var param = LinqExpression.Parameter(typeof(bool));
            var expression = LinqExpression.IfThenElse(param, param, param);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Parameter()
        {
            var expression = LinqExpression.Parameter(typeof(object));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Parameter_with_name()
        {
            var expression = LinqExpression.Parameter(typeof(object), "param");

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Unary()
        {
            var expression = LinqExpression.ArrayLength(LinqExpression.Parameter(typeof(int[])));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Factorial()
        {
            var value = LinqExpression.Parameter(typeof(int), "value");
            var result = LinqExpression.Parameter(typeof(int), "result");
            var label = LinqExpression.Label(typeof(int), "label");
            var one = LinqExpression.Constant(1);
            var expression = LinqExpression.Block(
                new[] { result },
                LinqExpression.Assign(
                    result,
                    one),
                LinqExpression.Loop(
                    LinqExpression.Condition(
                        LinqExpression.GreaterThan(
                            value,
                            one),
                        LinqExpression.MultiplyAssign(
                            result,
                            LinqExpression.PostDecrementAssign(
                                value)),
                        LinqExpression.Break(
                            label,
                            result),
                        typeof(void)),
                    label));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void TypeArguments()
        {
            var expression = LinqExpression.Parameter(typeof(IEquatable<int>));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Break()
        {
            var expression = LinqExpression.Break(
                LinqExpression.Label(
                    typeof(void)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberInit_no_bindings()
        {
            var expression =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(C3)));


            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberInit_bind()
        {
            var expression =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(C3)),
                    LinqExpression.Bind(
                        typeof(C3).GetField("F1"),
                        LinqExpression.Constant(0L)));


            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberInit_list_bind()
        {
            var expression =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(C3)),
                    LinqExpression.ListBind(
                        typeof(C3).GetProperty("P1"),
                        LinqExpression.ElementInit(
                            typeof(List<long>).GetMethod("Add"),
                            LinqExpression.Constant(0L))));


            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberInit_member_bind()
        {
            var expression =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(C3)),
                    LinqExpression.MemberBind(
                        typeof(C3).GetField("F2"),
                        LinqExpression.Bind(
                            typeof(C3).GetField("F1"),
                            LinqExpression.Constant(0L))));


            ShouldRoundrip(expression);
        }

        private static void ShouldRoundrip(LinqExpression original)
        {
            using var g = new GraphEngine.Graph();
            var s = g.CreateBlankNode();

            new SerialisingVisitor(s).Visit(original);
            Console.WriteLine("Graph generated from visitor");
            Console.WriteLine("-------------------------");
            new CompressingTurtleWriter(WriterCompressionLevel.Medium).Save(g, Console.Out, true);
            Console.WriteLine();
            Console.WriteLine();

            var processed = GraphEngine.Expression.Parse(s).LinqExpression;
            Console.WriteLine("Expression parsed from graph");
            Console.WriteLine("-------------------------");
            Console.WriteLine(processed.GetDebugView());

            processed.Should().Be(original);
        }
    }
}
