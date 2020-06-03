// MIT License, Copyright 2020 Samu Lang

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
            this.RdfType = Vocabulary.Empty;
        }

        public override Linq.Expression LinqExpression => Linq.Expression.Empty();
    }
}
