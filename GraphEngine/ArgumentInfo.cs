// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using Microsoft.CSharp.RuntimeBinder;

public class ArgumentInfo(NodeWithGraph node) : Node(node)
{
    internal CSharpArgumentInfo Info =>
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);

    internal static ArgumentInfo Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new ArgumentInfo(node)
    };
}
