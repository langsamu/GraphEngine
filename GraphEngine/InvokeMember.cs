// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Runtime.CompilerServices;
using CSharp = Microsoft.CSharp.RuntimeBinder;

public class InvokeMember(NodeWithGraph node) : Binder(node, Vocabulary.InvokeMember)
{
    internal override CallSiteBinder SystemBinder => CSharp.Binder.InvokeMember(
        CSharp.CSharpBinderFlags.None,
        this.Name,
        null,
        null,
        from a in this.Arguments select a.Info);
}
