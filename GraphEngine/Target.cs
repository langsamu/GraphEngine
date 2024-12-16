// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Target : Node
{
    private static readonly Dictionary<INode, Linq.LabelTarget> Cache = [];

    [DebuggerStepThrough]
    internal Target(NodeWithGraph node)
        : base(node)
    {
    }

    public Type? Type
    {
        get => this.GetOptional(TargetType, Type.Parse);

        set => this.SetOptional(TargetType, value);
    }

    public string? Name
    {
        get => this.GetOptional(TargetName, AsString);

        set => this.SetOptional(TargetName, value);
    }

    public Linq.LabelTarget LinqTarget
    {
        get
        {
            if (!Cache.TryGetValue(this, out var label))
            {
                Cache[this] = label = this switch
                {
                    { Type: not null, Name: not null } => Linq.Expression.Label(this.Type.SystemType, this.Name),
                    { Type: not null } => Linq.Expression.Label(this.Type.SystemType),
                    { Name: not null } => Linq.Expression.Label(this.Name),
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
