// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System;
using VDS.RDF;

public abstract partial class Expression
{
    private static Expression ParseResource(NodeWithGraph node) => Vocabulary.RdfType.ObjectOf(node) switch
    {
        INode t when t.Equals(Vocabulary.ArrayAccess) => new ArrayAccess(node),
        INode t when t.Equals(Vocabulary.ArrayIndex) => new ArrayIndex(node),
        INode t when t.Equals(Vocabulary.Block) => new Block(node),
        INode t when t.Equals(Vocabulary.Binary) => new Binary(node),
        INode t when t.Equals(Vocabulary.Break) => new Break(node),
        INode t when t.Equals(Vocabulary.Call) => new Call(node),
        INode t when t.Equals(Vocabulary.ClearDebugInfo) => new ClearDebugInfo(node),
        INode t when t.Equals(Vocabulary.Condition) => new Condition(node),
        INode t when t.Equals(Vocabulary.Constant) => new Constant(node),
        INode t when t.Equals(Vocabulary.Continue) => new Continue(node),
        INode t when t.Equals(Vocabulary.DebugInfo) => new DebugInfo(node),
        INode t when t.Equals(Vocabulary.Default) => new Default(node),
        INode t when t.Equals(Vocabulary.Dynamic) => new Dynamic(node),
        INode t when t.Equals(Vocabulary.Empty) => new Empty(node),
        INode t when t.Equals(Vocabulary.Field) => new Field(node),
        INode t when t.Equals(Vocabulary.Goto) => new Goto(node),
        INode t when t.Equals(Vocabulary.IfThen) => new IfThen(node),
        INode t when t.Equals(Vocabulary.IfThenElse) => new IfThenElse(node),
        INode t when t.Equals(Vocabulary.Invoke) => new Invoke(node),
        INode t when t.Equals(Vocabulary.Label) => new Label(node),
        INode t when t.Equals(Vocabulary.Lambda) => new Lambda(node),
        INode t when t.Equals(Vocabulary.ListInit) => new ListInit(node),
        INode t when t.Equals(Vocabulary.Loop) => new Loop(node),
        INode t when t.Equals(Vocabulary.MemberInit) => new MemberInit(node),
        INode t when t.Equals(Vocabulary.New) => new New(node),
        INode t when t.Equals(Vocabulary.NewArrayBounds) => new NewArrayBounds(node),
        INode t when t.Equals(Vocabulary.NewArrayInit) => new NewArrayInit(node),
        INode t when t.Equals(Vocabulary.Parameter) => new Parameter(node),
        INode t when t.Equals(Vocabulary.Property) => new Property(node),
        INode t when t.Equals(Vocabulary.PropertyOrField) => new PropertyOrField(node),
        INode t when t.Equals(Vocabulary.ReferenceEqual) => new ReferenceEqual(node),
        INode t when t.Equals(Vocabulary.ReferenceNotEqual) => new ReferenceNotEqual(node),
        INode t when t.Equals(Vocabulary.Rethrow) => new Rethrow(node),
        INode t when t.Equals(Vocabulary.Return) => new Return(node),
        INode t when t.Equals(Vocabulary.RuntimeVariables) => new RuntimeVariables(node),
        INode t when t.Equals(Vocabulary.Switch) => new Switch(node),
        INode t when t.Equals(Vocabulary.Throw) => new Throw(node),
        INode t when t.Equals(Vocabulary.Try) => new Try(node),
        INode t when t.Equals(Vocabulary.TypeBinary) => new TypeBinary(node),
        INode t when t.Equals(Vocabulary.Unary) => new Unary(node),
        INode t when t.Equals(Vocabulary.Variable) => new Variable(node),

        _ => throw new Exception($"unknown type {Vocabulary.RdfType.ObjectOf(node)} on node {node}")
    };
}
