// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using VDS.RDF;

    public abstract partial class Expression
    {
        private static Expression ParseResource(INode node)
        {
            var type = Vocabulary.RdfType.ObjectOf(node);
            switch (type)
            {
                case INode t when t.Equals(Vocabulary.Add): return new Add(node);
                case INode t when t.Equals(Vocabulary.AddAssign): return new AddAssign(node);
                case INode t when t.Equals(Vocabulary.AddAssignChecked): return new AddAssignChecked(node);
                case INode t when t.Equals(Vocabulary.AddChecked): return new AddChecked(node);
                case INode t when t.Equals(Vocabulary.And): return new And(node);
                case INode t when t.Equals(Vocabulary.AndAlso): return new AndAlso(node);
                case INode t when t.Equals(Vocabulary.AndAssign): return new AndAssign(node);
                case INode t when t.Equals(Vocabulary.ArrayAccess): return new ArrayAccess(node);
                case INode t when t.Equals(Vocabulary.ArrayIndex): return new ArrayIndex(node);
                case INode t when t.Equals(Vocabulary.ArrayLength): return new ArrayLength(node);
                case INode t when t.Equals(Vocabulary.Assign): return new Assign(node);
                case INode t when t.Equals(Vocabulary.Block): return new Block(node);
                case INode t when t.Equals(Vocabulary.Break): return new Break(node);
                case INode t when t.Equals(Vocabulary.Call): return new Call(node);
                case INode t when t.Equals(Vocabulary.Coalesce): return new Coalesce(node);
                case INode t when t.Equals(Vocabulary.Condition): return new Condition(node);
                case INode t when t.Equals(Vocabulary.Constant): return new Constant(node);
                case INode t when t.Equals(Vocabulary.Continue): return new Continue(node);
                case INode t when t.Equals(Vocabulary.Convert): return new Convert(node);
                case INode t when t.Equals(Vocabulary.ConvertChecked): return new ConvertChecked(node);
                case INode t when t.Equals(Vocabulary.Decrement): return new Decrement(node);
                case INode t when t.Equals(Vocabulary.Default): return new Default(node);
                case INode t when t.Equals(Vocabulary.Divide): return new Divide(node);
                case INode t when t.Equals(Vocabulary.DivideAssign): return new DivideAssign(node);
                case INode t when t.Equals(Vocabulary.Empty): return new Empty(node);
                case INode t when t.Equals(Vocabulary.Equal): return new Equal(node);
                case INode t when t.Equals(Vocabulary.ExclusiveOr): return new ExclusiveOr(node);
                case INode t when t.Equals(Vocabulary.ExclusiveOrAssign): return new ExclusiveOrAssign(node);
                case INode t when t.Equals(Vocabulary.Goto): return new Goto(node);
                case INode t when t.Equals(Vocabulary.GreaterThan): return new GreaterThan(node);
                case INode t when t.Equals(Vocabulary.GreaterThanOrEqual): return new GreaterThanOrEqual(node);
                case INode t when t.Equals(Vocabulary.Increment): return new Increment(node);
                case INode t when t.Equals(Vocabulary.Invoke): return new Invoke(node);
                case INode t when t.Equals(Vocabulary.IsFalse): return new IsFalse(node);
                case INode t when t.Equals(Vocabulary.IsTrue): return new IsTrue(node);
                case INode t when t.Equals(Vocabulary.Label): return new Label(node);
                case INode t when t.Equals(Vocabulary.Lambda): return new Lambda(node);
                case INode t when t.Equals(Vocabulary.LeftShift): return new LeftShift(node);
                case INode t when t.Equals(Vocabulary.LeftShiftAssign): return new LeftShiftAssign(node);
                case INode t when t.Equals(Vocabulary.LessThan): return new LessThan(node);
                case INode t when t.Equals(Vocabulary.LessThanOrEqual): return new LessThanOrEqual(node);
                case INode t when t.Equals(Vocabulary.Loop): return new Loop(node);
                case INode t when t.Equals(Vocabulary.Modulo): return new Modulo(node);
                case INode t when t.Equals(Vocabulary.ModuloAssign): return new ModuloAssign(node);
                case INode t when t.Equals(Vocabulary.Multiply): return new Multiply(node);
                case INode t when t.Equals(Vocabulary.MultiplyAssign): return new MultiplyAssign(node);
                case INode t when t.Equals(Vocabulary.MultiplyAssignChecked): return new MultiplyAssignChecked(node);
                case INode t when t.Equals(Vocabulary.MultiplyChecked): return new MultiplyChecked(node);
                case INode t when t.Equals(Vocabulary.Negate): return new Negate(node);
                case INode t when t.Equals(Vocabulary.NegateChecked): return new NegateChecked(node);
                case INode t when t.Equals(Vocabulary.New): return new New(node);
                case INode t when t.Equals(Vocabulary.NewArrayBounds): return new NewArrayBounds(node);
                case INode t when t.Equals(Vocabulary.Not): return new Not(node);
                case INode t when t.Equals(Vocabulary.NotEqual): return new NotEqual(node);
                case INode t when t.Equals(Vocabulary.OnesComplement): return new OnesComplement(node);
                case INode t when t.Equals(Vocabulary.Or): return new Or(node);
                case INode t when t.Equals(Vocabulary.OrAssign): return new OrAssign(node);
                case INode t when t.Equals(Vocabulary.OrElse): return new OrElse(node);
                case INode t when t.Equals(Vocabulary.Parameter): return new Parameter(node);
                case INode t when t.Equals(Vocabulary.PostDecrementAssign): return new PostDecrementAssign(node);
                case INode t when t.Equals(Vocabulary.PostIncrementAssign): return new PostIncrementAssign(node);
                case INode t when t.Equals(Vocabulary.Power): return new Power(node);
                case INode t when t.Equals(Vocabulary.PowerAssign): return new PowerAssign(node);
                case INode t when t.Equals(Vocabulary.PreDecrementAssign): return new PreDecrementAssign(node);
                case INode t when t.Equals(Vocabulary.PreIncrementAssign): return new PreIncrementAssign(node);
                case INode t when t.Equals(Vocabulary.Quote): return new Quote(node);
                case INode t when t.Equals(Vocabulary.Return): return new Return(node);
                case INode t when t.Equals(Vocabulary.RightShift): return new RightShift(node);
                case INode t when t.Equals(Vocabulary.RightShiftAssign): return new RightShiftAssign(node);
                case INode t when t.Equals(Vocabulary.Subtract): return new Subtract(node);
                case INode t when t.Equals(Vocabulary.SubtractAssign): return new SubtractAssign(node);
                case INode t when t.Equals(Vocabulary.SubtractAssignChecked): return new SubtractAssignChecked(node);
                case INode t when t.Equals(Vocabulary.SubtractChecked): return new SubtractChecked(node);
                case INode t when t.Equals(Vocabulary.Throw): return new Throw(node);
                case INode t when t.Equals(Vocabulary.Try): return new Try(node);
                case INode t when t.Equals(Vocabulary.TypeAs): return new TypeAs(node);
                case INode t when t.Equals(Vocabulary.UnaryPlus): return new UnaryPlus(node);
                case INode t when t.Equals(Vocabulary.Unbox): return new Unbox(node);
                case INode t when t.Equals(Vocabulary.Variable): return new Variable(node);

                default: throw new Exception($"unknown type {type} on node {node}");
            }
        }
    }
}
