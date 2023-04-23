// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Microsoft.CSharp.RuntimeBinder;

    public class BinaryOperation : Binder
    {
        [DebuggerStepThrough]
        public BinaryOperation(NodeWithGraph node)
            : base(node)
        {
            this.RdfType = Vocabulary.BinaryOperation;
        }

        internal override CallSiteBinder SystemBinder =>
            Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(
                CSharpBinderFlags.None,
                this.ExpressionType.LinqExpressionType,
                null,
                from a in this.Arguments select a.Info);
    }
}
