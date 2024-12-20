// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Reflection;

public class Member(NodeWithGraph node) : Node(node)
{
    public Type Type
    {
        get => GetRequired(MemberType, Type.Parse);

        set => SetRequired(MemberType, value);
    }

    public string Name
    {
        get => GetRequired(MemberName, AsString);

        set => SetRequired(MemberName, value);
    }

    public MemberInfo ReflectionMember => Type.SystemType.GetMember(Name).Single();

    internal static Member Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new Member(node)
    };
}
