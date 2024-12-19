// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Runtime.CompilerServices;

public abstract class Binder(NodeWithGraph node, INode type) : Node(node, type)
{
    public string Name
    {
        get => this.GetRequired(BinderName, AsString);

        set => this.SetOptional(BinderName, value);
    }

    public ExpressionType? ExpressionType
    {
        get => this.GetOptional(BinderExpressionType, ExpressionType.Parse);

        set => this.SetOptional(BinderExpressionType, value);
    }

    public ICollection<ArgumentInfo> Arguments => this.Collection(BinderArguments, ArgumentInfo.Parse);

    internal abstract CallSiteBinder SystemBinder { get; }

    internal static Binder Parse(NodeWithGraph node)
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }

        return Vocabulary.RdfType.ObjectOf(node) switch
        {
            INode t when t.Equals(Vocabulary.InvokeMember) => new InvokeMember(node),
            INode t when t.Equals(Vocabulary.BinaryOperation) => new BinaryOperation(node),

            null => throw new Exception($"type not found on node {node}"),
            INode t => throw new Exception($"unknown binder type {t} on node {node}"),
        };
    }
}
