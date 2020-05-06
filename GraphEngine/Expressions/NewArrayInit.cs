// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    // TODO: Implement
    public class NewArrayInit : Expression
    {
        [DebuggerStepThrough]
        internal NewArrayInit(INode node)
            : base(node)
        {
            throw new NotImplementedException();
        }

        public override Linq.Expression LinqExpression => throw new InvalidOperationException();
    }
}
