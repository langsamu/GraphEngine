// MIT License, Copyright 2020 Samu Lang

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
                case INode t when t.Equals(Vocabulary.ArrayAccess): return new ArrayAccess(node);
                case INode t when t.Equals(Vocabulary.ArrayIndex): return new ArrayIndex(node);
                case INode t when t.Equals(Vocabulary.ArrayLength): return new ArrayLength(node);
                case INode t when t.Equals(Vocabulary.Block): return new Block(node);
                case INode t when t.Equals(Vocabulary.Binary): return new Binary(node);
                case INode t when t.Equals(Vocabulary.Break): return new Break(node);
                case INode t when t.Equals(Vocabulary.Call): return new Call(node);
                case INode t when t.Equals(Vocabulary.Condition): return new Condition(node);
                case INode t when t.Equals(Vocabulary.Constant): return new Constant(node);
                case INode t when t.Equals(Vocabulary.Continue): return new Continue(node);
                case INode t when t.Equals(Vocabulary.Convert): return new Convert(node);
                case INode t when t.Equals(Vocabulary.ConvertChecked): return new ConvertChecked(node);
                case INode t when t.Equals(Vocabulary.DebugInfo): return new DebugInfo(node);
                case INode t when t.Equals(Vocabulary.Decrement): return new Decrement(node);
                case INode t when t.Equals(Vocabulary.Default): return new Default(node);
                case INode t when t.Equals(Vocabulary.Dynamic): return new Dynamic(node);
                case INode t when t.Equals(Vocabulary.Empty): return new Empty(node);
                case INode t when t.Equals(Vocabulary.Field): return new Field(node);
                case INode t when t.Equals(Vocabulary.Goto): return new Goto(node);
                case INode t when t.Equals(Vocabulary.IfThen): return new IfThen(node);
                case INode t when t.Equals(Vocabulary.IfThenElse): return new IfThenElse(node);
                case INode t when t.Equals(Vocabulary.Increment): return new Increment(node);
                case INode t when t.Equals(Vocabulary.Invoke): return new Invoke(node);
                case INode t when t.Equals(Vocabulary.IsFalse): return new IsFalse(node);
                case INode t when t.Equals(Vocabulary.IsTrue): return new IsTrue(node);
                case INode t when t.Equals(Vocabulary.Label): return new Label(node);
                case INode t when t.Equals(Vocabulary.Lambda): return new Lambda(node);
                case INode t when t.Equals(Vocabulary.ListInit): return new ListInit(node);
                case INode t when t.Equals(Vocabulary.Loop): return new Loop(node);
                case INode t when t.Equals(Vocabulary.MemberInit): return new MemberInit(node);
                case INode t when t.Equals(Vocabulary.Negate): return new Negate(node);
                case INode t when t.Equals(Vocabulary.NegateChecked): return new NegateChecked(node);
                case INode t when t.Equals(Vocabulary.New): return new New(node);
                case INode t when t.Equals(Vocabulary.NewArrayBounds): return new NewArrayBounds(node);
                case INode t when t.Equals(Vocabulary.NewArrayInit): return new NewArrayInit(node);
                case INode t when t.Equals(Vocabulary.Not): return new Not(node);
                case INode t when t.Equals(Vocabulary.OnesComplement): return new OnesComplement(node);
                case INode t when t.Equals(Vocabulary.Parameter): return new Parameter(node);
                case INode t when t.Equals(Vocabulary.PostDecrementAssign): return new PostDecrementAssign(node);
                case INode t when t.Equals(Vocabulary.PostIncrementAssign): return new PostIncrementAssign(node);
                case INode t when t.Equals(Vocabulary.PreDecrementAssign): return new PreDecrementAssign(node);
                case INode t when t.Equals(Vocabulary.PreIncrementAssign): return new PreIncrementAssign(node);
                case INode t when t.Equals(Vocabulary.Property): return new Property(node);
                case INode t when t.Equals(Vocabulary.PropertyOrField): return new PropertyOrField(node);
                case INode t when t.Equals(Vocabulary.Quote): return new Quote(node);
                case INode t when t.Equals(Vocabulary.ReferenceEqual): return new ReferenceEqual(node);
                case INode t when t.Equals(Vocabulary.ReferenceNotEqual): return new ReferenceNotEqual(node);
                case INode t when t.Equals(Vocabulary.Return): return new Return(node);
                case INode t when t.Equals(Vocabulary.RuntimeVariables): return new RuntimeVariables(node);
                case INode t when t.Equals(Vocabulary.Switch): return new Switch(node);
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
