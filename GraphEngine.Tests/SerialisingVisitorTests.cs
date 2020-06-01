// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF.Writing;
    using Linq = System.Linq.Expressions;
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

        [TestMethod]
        public void ArrayIndex_index()
        {
            var expression =
                LinqExpression.ArrayIndex(
                    LinqExpression.Parameter(
                        typeof(int[])),
                    LinqExpression.Parameter(
                        typeof(int)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void ArrayIndex_indexes()
        {
            var expression =
                LinqExpression.ArrayIndex(
                    LinqExpression.Parameter(
                        typeof(int[])),
                    new[]
                    {
                        LinqExpression.Parameter(
                            typeof(int)),
                    });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Binary()
        {
            var param = LinqExpression.Parameter(typeof(int));
            var expression = LinqExpression.Add(param, param);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Dynamic_InvokeMember()
        {
            var expression =
                LinqExpression.Dynamic(
                    Binder.InvokeMember(
                        CSharpBinderFlags.None,
                        "ToString",
                        null,
                        null,
                        new[]
                        {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        }),
                    typeof(object),
                    LinqExpression.Constant(0L));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Dynamic_BinaryOperation()
        {
            var expression =
                LinqExpression.Dynamic(
                    Binder.BinaryOperation(
                        CSharpBinderFlags.None,
                        Linq.ExpressionType.Add,
                        null,
                        new[]
                        {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        }),
                    typeof(object),
                    LinqExpression.Constant(2L),
                    LinqExpression.Constant(3L));

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
        public void Break()
        {
            var expression = LinqExpression.Break(
                LinqExpression.Label(
                    typeof(void)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_instance_no_types_arguments()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.InstanceMethodWithArgument),
                    Array.Empty<Type>(),
                    new[]
                    {
                        LinqExpression.Constant(
                            0L,
                            typeof(long)),
                    });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_instance_no_types_no_arguments()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.InstanceMethod),
                    Array.Empty<Type>(),
                    Array.Empty<LinqExpression>());

            using var g = new GraphEngine.Graph();
            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_instance_types_arguments()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.GenericInstanceMethodWithArgument),
                    new[]
                    {
                        typeof(object),
                    },
                    new[]
                    {
                        LinqExpression.Constant(
                            0L,
                            typeof(long)),
                    });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_instance_types_no_arguments()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.GenericInstanceMethod),
                    new[]
                    {
                        typeof(object),
                    },
                    Array.Empty<LinqExpression>());

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_static_no_types_arguments()
        {
            var expression =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.StaticMethodWithArgument),
                    Array.Empty<Type>(),
                    new[]
                    {
                        LinqExpression.Constant(
                            0L,
                            typeof(long)),
                    });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_static_no_types_no_arguments()
        {
            var expression =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.StaticMethod),
                    Array.Empty<Type>(),
                    Array.Empty<LinqExpression>());

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_static_types_arguments()
        {
            var expression =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.GenericStaticMethodWithArgument),
                    new[]
                    {
                        typeof(object),
                    },
                    new[]
                    {
                        LinqExpression.Constant(
                            0L,
                            typeof(long)),
                    });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_static_types_no_arguments()
        {
            var expression =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.GenericStaticMethod),
                    new[]
                    {
                        typeof(object),
                    },
                    Array.Empty<LinqExpression>());

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
                LinqExpression.Parameter(typeof(SampleDerivedClass)),
                LinqExpression.Parameter(typeof(SampleClass)),
                typeof(SampleClass));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void DebugInfo()
        {
            var expression =
                LinqExpression.DebugInfo(
                    LinqExpression.SymbolDocument(
                        string.Empty),
                    1,
                    1,
                    1,
                    1);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void DebugInfo_language()
        {
            var expression =
                LinqExpression.DebugInfo(
                    LinqExpression.SymbolDocument(
                        string.Empty,
                        new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a5")),
                    1,
                    1,
                    1,
                    1);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void DebugInfo_language_vendor()
        {
            var expression =
                LinqExpression.DebugInfo(
                    LinqExpression.SymbolDocument(
                        string.Empty,
                        new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a5"),
                        new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a6")),
                    1,
                    1,
                    1,
                    1);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void DebugInfo_language_vendor_type()
        {
            var expression =
                LinqExpression.DebugInfo(
                    LinqExpression.SymbolDocument(
                        string.Empty,
                        new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a5"),
                        new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a6"),
                        new Guid("61eac4f1-bb04-4197-a7bd-eb5749f343a7")),
                    1,
                    1,
                    1,
                    1);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void DebugInfo_clear()
        {
            var expression =
                LinqExpression.ClearDebugInfo(
                    LinqExpression.SymbolDocument(
                        string.Empty));

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
        public void ListInit()
        {
            var expression =
                LinqExpression.ListInit(
                    LinqExpression.New(
                        typeof(List<long>)),
                    LinqExpression.ElementInit(
                        typeof(List<long>).GetMethod("Add"),
                        LinqExpression.Constant(0L)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberAccess_field_expression()
        {
            var expression =
                LinqExpression.Field(
                    LinqExpression.Parameter(typeof(SampleClass)),
                    nameof(SampleClass.InstanceField));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberAccess_field_expression_type()
        {
            var expression =
                LinqExpression.Field(
                    LinqExpression.Parameter(typeof(SampleDerivedClass)),
                    typeof(SampleClass),
                    nameof(SampleClass.InstanceField));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberAccess_field_type()
        {
            var expression =
                LinqExpression.Field(
                    null,
                    typeof(SampleClass),
                    nameof(SampleClass.StaticField));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberAccess_property_expression()
        {
            var expression =
                LinqExpression.Property(
                    LinqExpression.Parameter(typeof(SampleClass)),
                    nameof(SampleClass.InstanceProperty));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberAccess_property_expression_type()
        {
            var expression =
                LinqExpression.Property(
                    LinqExpression.Parameter(typeof(SampleDerivedClass)),
                    typeof(SampleClass),
                    nameof(SampleClass.InstanceProperty));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberAccess_property_type()
        {
            var expression =
                LinqExpression.Property(
                    null,
                    typeof(SampleClass),
                    nameof(SampleClass.StaticProperty));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberAccess_property_expression_arguments()
        {
            var expression =
                LinqExpression.Property(
                    LinqExpression.Parameter(typeof(SampleClass)),
                    "Indexer",
                    LinqExpression.Parameter(typeof(int)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberInit_no_bindings()
        {
            var expression =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(SampleClass)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberInit_bind()
        {
            var expression =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    LinqExpression.Bind(
                        typeof(SampleClass).GetField(nameof(SampleClass.InstanceField)),
                        LinqExpression.Constant(string.Empty)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void MemberInit_list_bind()
        {
            var expression =
                LinqExpression.MemberInit(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    LinqExpression.ListBind(
                        typeof(SampleClass).GetProperty(nameof(SampleClass.ListProperty)),
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
                        typeof(SampleClass)),
                    LinqExpression.MemberBind(
                        typeof(SampleClass).GetField(nameof(SampleClass.ComplexField)),
                        LinqExpression.Bind(
                            typeof(SampleClass).GetField(nameof(SampleClass.InstanceField)),
                            LinqExpression.Constant(
                                string.Empty))));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void NewArrayBounds()
        {
            var expression =
                LinqExpression.NewArrayBounds(
                    typeof(long),
                    LinqExpression.Constant(0L));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void NewArrayInit()
        {
            var expression =
                LinqExpression.NewArrayInit(
                    typeof(long),
                    LinqExpression.Constant(0L));

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
        public void ReferenceEqual()
        {
            var expression =
                LinqExpression.ReferenceEqual(
                    LinqExpression.Parameter(
                        typeof(object)),
                    LinqExpression.Parameter(
                        typeof(object)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void ReferenceNotEqual()
        {
            var expression =
                LinqExpression.ReferenceNotEqual(
                    LinqExpression.Parameter(
                        typeof(object)),
                    LinqExpression.Parameter(
                        typeof(object)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void RuntimeVariables()
        {
            var expression =
                LinqExpression.RuntimeVariables(
                    LinqExpression.Parameter(
                        typeof(object)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Throw()
        {
            var expression =
                LinqExpression.Throw(null);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Throw_value()
        {
            var expression =
                LinqExpression.Throw(
                    LinqExpression.New(
                        typeof(ArgumentException)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Throw_type()
        {
            var expression =
                LinqExpression.Throw(
                    null,
                    typeof(Exception)); ;

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Throw_value_type()
        {
            var expression =
                LinqExpression.Throw(
                    LinqExpression.New(
                        typeof(ArgumentException)),
                    typeof(Exception));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Rethrow()
        {
            var expression =
                LinqExpression.Rethrow();

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Rethrow_type()
        {
            var expression =
                LinqExpression.Rethrow(
                    typeof(Exception));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void TypeArguments()
        {
            var expression = LinqExpression.Parameter(typeof(IEquatable<int>));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Unary()
        {
            var expression = LinqExpression.ArrayLength(LinqExpression.Parameter(typeof(int[])));

            ShouldRoundrip(expression);
        }

        private static void ShouldRoundrip(LinqExpression original)
        {
            using var g = new GraphEngine.Graph();
            var s = g.CreateBlankNode();

            new SerialisingVisitor(s).Visit(original);
            new CompressingTurtleWriter(WriterCompressionLevel.Medium).Save(g, Console.Out, true);
            Console.WriteLine();

            var processed = GraphEngine.Expression.Parse(s).LinqExpression;
            Console.WriteLine(processed.GetDebugView());

            processed.Should().Be(original);
        }
    }
}
