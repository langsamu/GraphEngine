// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Type(NodeWithGraph node) : Node(node)
{
    public string Name
    {
        get => GetRequired(TypeName, AsString);

        set => SetRequired(TypeName, value);
    }

    public ICollection<Type> Arguments => Collection(TypeArguments, Parse);

    public System.Type SystemType
    {
        get
        {
            var t = System.Type.GetType(Name) ?? throw new InvalidOperationException($"Type {Name} not found.");

            if (t.IsGenericTypeDefinition)
            {
                return t.MakeGenericType(Arguments.Select(arg => arg.SystemType).ToArray());
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
