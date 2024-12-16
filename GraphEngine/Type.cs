// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Type : Node
{
    [DebuggerStepThrough]
    public Type(NodeWithGraph node)
        : base(node)
    {
    }

    public string Name
    {
        get => this.GetRequired(TypeName, AsString);

        set => this.SetRequired(TypeName, value);
    }

    public ICollection<Type> Arguments => this.Collection(TypeArguments, Parse);

    public System.Type SystemType
    {
        get
        {
            var t = System.Type.GetType(this.Name) ?? throw new InvalidOperationException($"Type {this.Name} not found.");

            if (t.IsGenericTypeDefinition)
            {
                return t.MakeGenericType(this.Arguments.Select(arg => arg.SystemType).ToArray());
            }

            return t;
        }
    }

    internal static Type Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new Type(node)
    };
}
