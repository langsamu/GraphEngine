// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.CSharp.RuntimeBinder;
    using VDS.RDF;

    public class BinaryOperation : Binder
    {
        [DebuggerStepThrough]
        public BinaryOperation(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.BinaryOperation;
        }

        internal override System.Runtime.CompilerServices.CallSiteBinder SystemBinder =>
            Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(
                CSharpBinderFlags.None,
                this.ExpressionType.LinqExpressionType,
                null,
                this.Arguments.Select(a => a.Info));
    }
}
