// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class ArrayAccess : Expression
    {
        [DebuggerStepThrough]
        internal ArrayAccess(INode node)
            : base(node)
        {
        }

        public Expression Array => Vocabulary.ArrayAccessArray.ObjectsOf(this).Select(Parse).Single();

        public IEnumerable<Expression> Indexes => Vocabulary.ArrayAccessIndexes.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.ArrayAccess(this.Array.LinqExpression, this.Indexes.Select(i => i.LinqExpression));
    }
}
