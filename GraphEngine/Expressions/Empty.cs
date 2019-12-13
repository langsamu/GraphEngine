// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Empty : Expression
    {
        [DebuggerStepThrough]
        internal Empty(INode node)
            : base(node)
        {
        }

        public override Linq.Expression LinqExpression => Linq.Expression.Empty();
    }
}
