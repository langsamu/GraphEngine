// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

public class BinaryOperation(NodeWithGraph node) : Binder(node, Vocabulary.BinaryOperation)
{
    internal override CallSiteBinder SystemBinder =>
        Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(
            CSharpBinderFlags.None,
            ExpressionType.LinqExpressionType,
            null,
            from a in Arguments select a.Info);
}
