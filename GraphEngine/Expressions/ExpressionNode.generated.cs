﻿// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using VDS.RDF;

    public abstract partial class ExpressionNode
    {
        private static ExpressionNode ParseResource(INode node)
        {
            var type = Vocabulary.RdfType.ObjectOf(node);
            switch (type)
            {
                case INode t when t.Equals(Vocabulary.Add): return new AddExpressionNode(node);
                case INode t when t.Equals(Vocabulary.AddAssign): return new AddAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.AddAssignChecked): return new AddAssignCheckedExpressionNode(node);
                case INode t when t.Equals(Vocabulary.AddChecked): return new AddCheckedExpressionNode(node);
                case INode t when t.Equals(Vocabulary.And): return new AndExpressionNode(node);
                case INode t when t.Equals(Vocabulary.AndAlso): return new AndAlsoExpressionNode(node);
                case INode t when t.Equals(Vocabulary.AndAssign): return new AndAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.ArrayAccess): return new ArrayAccessExpressionNode(node);
                case INode t when t.Equals(Vocabulary.ArrayIndex): return new ArrayIndexExpressionNode(node);
                case INode t when t.Equals(Vocabulary.ArrayLength): return new ArrayLengthExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Assign): return new AssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Block): return new BlockExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Break): return new BreakExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Call): return new CallExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Coalesce): return new CoalesceExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Condition): return new ConditionExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Constant): return new ConstantExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Continue): return new ContinueExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Convert): return new ConvertExpressionNode(node);
                case INode t when t.Equals(Vocabulary.ConvertChecked): return new ConvertCheckedExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Decrement): return new DecrementExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Default): return new DefaultExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Divide): return new DivideExpressionNode(node);
                case INode t when t.Equals(Vocabulary.DivideAssign): return new DivideAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Empty): return new EmptyExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Equal): return new EqualExpressionNode(node);
                case INode t when t.Equals(Vocabulary.ExclusiveOr): return new ExclusiveOrExpressionNode(node);
                case INode t when t.Equals(Vocabulary.ExclusiveOrAssign): return new ExclusiveOrAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Goto): return new GotoExpressionNode(node);
                case INode t when t.Equals(Vocabulary.GreaterThan): return new GreaterThanExpressionNode(node);
                case INode t when t.Equals(Vocabulary.GreaterThanOrEqual): return new GreaterThanOrEqualExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Increment): return new IncrementExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Invoke): return new InvokeExpressionNode(node);
                case INode t when t.Equals(Vocabulary.IsFalse): return new IsFalseExpressionNode(node);
                case INode t when t.Equals(Vocabulary.IsTrue): return new IsTrueExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Label): return new LabelExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Lambda): return new LambdaExpressionNode(node);
                case INode t when t.Equals(Vocabulary.LeftShift): return new LeftShiftExpressionNode(node);
                case INode t when t.Equals(Vocabulary.LeftShiftAssign): return new LeftShiftAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.LessThan): return new LessThanExpressionNode(node);
                case INode t when t.Equals(Vocabulary.LessThanOrEqual): return new LessThanOrEqualExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Loop): return new LoopExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Modulo): return new ModuloExpressionNode(node);
                case INode t when t.Equals(Vocabulary.ModuloAssign): return new ModuloAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Multiply): return new MultiplyExpressionNode(node);
                case INode t when t.Equals(Vocabulary.MultiplyAssign): return new MultiplyAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.MultiplyAssignChecked): return new MultiplyAssignCheckedExpressionNode(node);
                case INode t when t.Equals(Vocabulary.MultiplyChecked): return new MultiplyCheckedExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Negate): return new NegateExpressionNode(node);
                case INode t when t.Equals(Vocabulary.NegateChecked): return new NegateCheckedExpressionNode(node);
                case INode t when t.Equals(Vocabulary.New): return new NewExpressionNode(node);
                case INode t when t.Equals(Vocabulary.NewArrayBounds): return new NewArrayBoundsExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Not): return new NotExpressionNode(node);
                case INode t when t.Equals(Vocabulary.NotEqual): return new NotEqualExpressionNode(node);
                case INode t when t.Equals(Vocabulary.OnesComplement): return new OnesComplementExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Or): return new OrExpressionNode(node);
                case INode t when t.Equals(Vocabulary.OrAssign): return new OrAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.OrElse): return new OrElseExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Parameter): return new ParameterExpressionNode(node);
                case INode t when t.Equals(Vocabulary.PostDecrementAssign): return new PostDecrementAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.PostIncrementAssign): return new PostIncrementAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Power): return new PowerExpressionNode(node);
                case INode t when t.Equals(Vocabulary.PowerAssign): return new PowerAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.PreDecrementAssign): return new PreDecrementAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.PreIncrementAssign): return new PreIncrementAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Quote): return new QuoteExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Return): return new ReturnExpressionNode(node);
                case INode t when t.Equals(Vocabulary.RightShift): return new RightShiftExpressionNode(node);
                case INode t when t.Equals(Vocabulary.RightShiftAssign): return new RightShiftAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Subtract): return new SubtractExpressionNode(node);
                case INode t when t.Equals(Vocabulary.SubtractAssign): return new SubtractAssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.SubtractAssignChecked): return new SubtractAssignCheckedExpressionNode(node);
                case INode t when t.Equals(Vocabulary.SubtractChecked): return new SubtractCheckedExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Throw): return new ThrowExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Try): return new TryExpressionNode(node);
                case INode t when t.Equals(Vocabulary.TypeAs): return new TypeAsExpressionNode(node);
                case INode t when t.Equals(Vocabulary.UnaryPlus): return new UnaryPlusExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Unbox): return new UnboxExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Variable): return new VariableExpressionNode(node);

                default: throw new Exception($"unknown type {type} on node {node}");
            }
        }
    }
}
