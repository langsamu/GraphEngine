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
        public void BinaryMethod()
        {
            var zero =
                LinqExpression.Default(
                    typeof(long));

            var expression =
                LinqExpression.Add(
                    zero,
                    zero,
                    typeof(SampleClass).GetMethod(nameof(SampleClass.Equal)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void BinaryLiftToNull()
        {
            var zero =
                LinqExpression.Default(
                    typeof(int?));

            var expression =
                LinqExpression.LessThan(
                    zero,
                    zero,
                    true,
                    null);

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void BinaryConversion()
        {
            var @object = typeof(object);
            var @null =
                LinqExpression.Default(
                    @object);

            var expression =
                LinqExpression.Coalesce(
                    @null,
                    @null,
                    LinqExpression.Lambda(
                        @null,
                        LinqExpression.Parameter(
                            @object)));

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
        public void Block_Expressions()
        {
            var expression =
                LinqExpression.Block(
                    LinqExpression.Default(typeof(string)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Block_ExpressionsVariables()
        {
            var expression =
                LinqExpression.Block(
                    new[]
                    {
                        LinqExpression.Parameter(
                            typeof(string)),
                    },
                    new[]
                    {
                        LinqExpression.Default(
                            typeof(string)),
                    });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Block_TypeExpressions()
        {
            var expression =
                LinqExpression.Block(
                    typeof(object),
                    LinqExpression.Default(typeof(string)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Block_TypeExpressionsVariables()
        {
            var expression =
                LinqExpression.Block(
                    typeof(object),
                    new[]
                    {
                        LinqExpression.Parameter(
                            typeof(string)),
                    },
                    new[]
                    {
                        LinqExpression.Default(
                            typeof(string)),
                    });

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
        public void Call_Method()
        {
            var expression =
                LinqExpression.Call(
                    typeof(SampleClass).GetMethod(nameof(SampleClass.StaticMethod)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Method_Arguments()
        {
            var expression =
                LinqExpression.Call(
                    typeof(SampleClass).GetMethod(nameof(SampleClass.StaticMethodWithArgument)),
                    LinqExpression.Constant(0L));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Method_TypeArguments()
        {
            var expression =
                LinqExpression.Call(
                    typeof(SampleClass).GetMethod(nameof(SampleClass.GenericStaticMethod)).MakeGenericMethod(typeof(object)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Method_TypeArguments_Arguments()
        {
            var expression =
                LinqExpression.Call(
                    typeof(SampleClass).GetMethod(nameof(SampleClass.GenericStaticMethodWithArgument)).MakeGenericMethod(typeof(object)),
                    LinqExpression.Constant(0L));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Method_Instance()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    typeof(SampleClass).GetMethod(nameof(SampleClass.InstanceMethod)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Method_Instance_Arguments()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    typeof(SampleClass).GetMethod(nameof(SampleClass.InstanceMethodWithArgument)),
                    LinqExpression.Constant(0L));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Method_Instance_TypeArguments()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    typeof(SampleClass).GetMethod(nameof(SampleClass.GenericInstanceMethod)).MakeGenericMethod(typeof(object)));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Method_Instance_TypeArguments_Arguments()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    typeof(SampleClass).GetMethod(nameof(SampleClass.GenericInstanceMethodWithArgument)).MakeGenericMethod(typeof(object)),
                    LinqExpression.Constant(0L));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Type()
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
        public void Call_Type_Arguments()
        {
            var expression =
                LinqExpression.Call(
                    typeof(SampleClass),
                    nameof(SampleClass.StaticMethodWithArgument),
                    Array.Empty<Type>(),
                    new[]
                    {
                        LinqExpression.Constant(0L),
                    });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Type_TypeArguments()
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
        public void Call_Type_TypeArguments_Arguments()
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
                        LinqExpression.Constant(0L),
                    });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Default()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.InstanceMethod),
                    Array.Empty<Type>(),
                    Array.Empty<LinqExpression>());

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Default_Arguments()
        {
            var expression =
                LinqExpression.Call(
                    LinqExpression.New(
                        typeof(SampleClass)),
                    nameof(SampleClass.InstanceMethodWithArgument),
                    Array.Empty<Type>(),
                    new[]
                    {
                        LinqExpression.Constant(0L),
                    });

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Call_Default_TypeArguments()
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
        public void Call_Instance_TypeArgumentss_Arguments()
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
                        LinqExpression.Constant(0L),
                    });

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
        public void Lambda()
        {
            var expression =
                LinqExpression.Lambda(LinqExpression.Empty());

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
                    typeof(Exception));

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
        public void TypeBinary_equal()
        {
            var expression =
                LinqExpression.TypeEqual(
                    LinqExpression.Empty(),
                    typeof(object));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void TypeBinary_is()
        {
            var expression =
                LinqExpression.TypeIs(
                    LinqExpression.Empty(),
                    typeof(object));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void Unary()
        {
            var expression = LinqExpression.ArrayLength(LinqExpression.Parameter(typeof(int[])));

            ShouldRoundrip(expression);
        }

        [TestMethod]
        public void UnaryMethod()
        {
            var expression =
                LinqExpression.Negate(
                    LinqExpression.Default(
                        typeof(bool)),
                    typeof(SampleClass).GetMethod(nameof(SampleClass.StaticFunctionWithArgument)));

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
