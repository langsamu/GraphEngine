// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System;
using System.Diagnostics;
using Microsoft.CSharp.RuntimeBinder;

public class ArgumentInfo : Node
{
    [DebuggerStepThrough]
    public ArgumentInfo(NodeWithGraph node)
        : base(node)
    {
    }

    internal CSharpArgumentInfo Info =>
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);

    internal static ArgumentInfo Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new ArgumentInfo(node)
    };
}
