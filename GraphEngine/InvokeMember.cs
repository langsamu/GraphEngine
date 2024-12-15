// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using CSharp = Microsoft.CSharp.RuntimeBinder;

public class InvokeMember : Binder
{
    [DebuggerStepThrough]
    public InvokeMember(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.InvokeMember;

    internal override CallSiteBinder SystemBinder => CSharp.Binder.InvokeMember(
        CSharp.CSharpBinderFlags.None,
        this.Name,
        null,
        null,
        from a in this.Arguments select a.Info);
}
