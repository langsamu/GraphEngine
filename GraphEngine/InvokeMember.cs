// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.CSharp.RuntimeBinder;
    using VDS.RDF;

    public class InvokeMember : Binder
    {
        [DebuggerStepThrough]
        public InvokeMember(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.InvokeMember;
        }

        internal override System.Runtime.CompilerServices.CallSiteBinder SystemBinder =>
            Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(
                CSharpBinderFlags.None,
                this.Name,
                null,
                null,
                this.Arguments.Select(a => a.Info));
    }
}
