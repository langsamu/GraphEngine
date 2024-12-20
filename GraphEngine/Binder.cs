// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Runtime.CompilerServices;

public abstract class Binder(NodeWithGraph node, INode type) : Node(node, type)
{
    public string Name
    {
        get => GetRequired(BinderName, AsString);

        set => SetOptional(BinderName, value);
    }

    public ExpressionType? ExpressionType
    {
        get => GetOptional(BinderExpressionType, ExpressionType.Parse);

        set => SetOptional(BinderExpressionType, value);
    }

    public ICollection<ArgumentInfo> Arguments => Collection(BinderArguments, ArgumentInfo.Parse);

    internal abstract CallSiteBinder SystemBinder { get; }

    internal static Binder Parse(NodeWithGraph node)
    {
        ArgumentNullException.ThrowIfNull(node);

        return Vocabulary.RdfType.ObjectOf(node) switch
        {
            INode t when t.Equals(Vocabulary.InvokeMember) => new InvokeMember(node),
            INode t when t.Equals(Vocabulary.BinaryOperation) => new BinaryOperation(node),

            null => throw new Exception($"type not found on node {node}"),
            var t => throw new Exception($"unknown binder type {t} on node {node}"),
        };
    }
}
