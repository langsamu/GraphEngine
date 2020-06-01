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
                case INode t when t.Equals(Vocabulary.Block): return new Block(node);
                case INode t when t.Equals(Vocabulary.Binary): return new Binary(node);
                case INode t when t.Equals(Vocabulary.Break): return new Break(node);
                case INode t when t.Equals(Vocabulary.Call): return new Call(node);
                case INode t when t.Equals(Vocabulary.ClearDebugInfo): return new ClearDebugInfo(node);
                case INode t when t.Equals(Vocabulary.Condition): return new Condition(node);
                case INode t when t.Equals(Vocabulary.Constant): return new Constant(node);
                case INode t when t.Equals(Vocabulary.Continue): return new Continue(node);
                case INode t when t.Equals(Vocabulary.DebugInfo): return new DebugInfo(node);
                case INode t when t.Equals(Vocabulary.Default): return new Default(node);
                case INode t when t.Equals(Vocabulary.Dynamic): return new Dynamic(node);
                case INode t when t.Equals(Vocabulary.Empty): return new Empty(node);
                case INode t when t.Equals(Vocabulary.Field): return new Field(node);
                case INode t when t.Equals(Vocabulary.Goto): return new Goto(node);
                case INode t when t.Equals(Vocabulary.IfThen): return new IfThen(node);
                case INode t when t.Equals(Vocabulary.IfThenElse): return new IfThenElse(node);
                case INode t when t.Equals(Vocabulary.Invoke): return new Invoke(node);
                case INode t when t.Equals(Vocabulary.Label): return new Label(node);
                case INode t when t.Equals(Vocabulary.Lambda): return new Lambda(node);
                case INode t when t.Equals(Vocabulary.ListInit): return new ListInit(node);
                case INode t when t.Equals(Vocabulary.Loop): return new Loop(node);
                case INode t when t.Equals(Vocabulary.MemberInit): return new MemberInit(node);
                case INode t when t.Equals(Vocabulary.New): return new New(node);
                case INode t when t.Equals(Vocabulary.NewArrayBounds): return new NewArrayBounds(node);
                case INode t when t.Equals(Vocabulary.NewArrayInit): return new NewArrayInit(node);
                case INode t when t.Equals(Vocabulary.Parameter): return new Parameter(node);
                case INode t when t.Equals(Vocabulary.Property): return new Property(node);
                case INode t when t.Equals(Vocabulary.PropertyOrField): return new PropertyOrField(node);
                case INode t when t.Equals(Vocabulary.ReferenceEqual): return new ReferenceEqual(node);
                case INode t when t.Equals(Vocabulary.ReferenceNotEqual): return new ReferenceNotEqual(node);
                case INode t when t.Equals(Vocabulary.Rethrow): return new Rethrow(node);
                case INode t when t.Equals(Vocabulary.Return): return new Return(node);
                case INode t when t.Equals(Vocabulary.RuntimeVariables): return new RuntimeVariables(node);
                case INode t when t.Equals(Vocabulary.Switch): return new Switch(node);
                case INode t when t.Equals(Vocabulary.Throw): return new Throw(node);
                case INode t when t.Equals(Vocabulary.Try): return new Try(node);
                case INode t when t.Equals(Vocabulary.TypeBinary): return new TypeBinary(node);
                case INode t when t.Equals(Vocabulary.Unary): return new Unary(node);
                case INode t when t.Equals(Vocabulary.Variable): return new Variable(node);

                default: throw new Exception($"unknown type {type} on node {node}");
            }
        }
    }
}
