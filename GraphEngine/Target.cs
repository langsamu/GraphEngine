// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Target(NodeWithGraph node) : Node(node)
{
    private static readonly Dictionary<INode, Linq.LabelTarget> Cache = [];

    public Type? Type
    {
        get => GetOptional(TargetType, Type.Parse);

        set => SetOptional(TargetType, value);
    }

    public string? Name
    {
        get => GetOptional(TargetName, AsString);

        set => SetOptional(TargetName, value);
    }

    public Linq.LabelTarget LinqTarget
    {
        get
        {
            if (!Cache.TryGetValue(this, out var label))
            {
                Cache[this] = label = this switch
                {
                    { Type: not null, Name: not null } => Linq.Expression.Label(Type.SystemType, Name),
                    { Type: not null } => Linq.Expression.Label(Type.SystemType),
                    { Name: not null } => Linq.Expression.Label(Name),
                    _ => Linq.Expression.Label()
                };
            }

            return label;
        }
    }

    internal static Target Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new Target(node)
    };
}
