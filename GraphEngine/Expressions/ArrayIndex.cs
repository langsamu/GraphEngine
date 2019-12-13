// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class ArrayIndex : Expression
    {
        [DebuggerStepThrough]
        internal ArrayIndex(INode node)
            : base(node)
        {
        }

        public Expression Array => Vocabulary.ArrayIndexArray.ObjectsOf(this).Select(Parse).Single();

        public Expression Index => Vocabulary.ArrayIndexIndex.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public IEnumerable<Expression> Indexes => Vocabulary.ArrayIndexIndexes.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public override Linq.Expression LinqExpression
        {
            get
            {
                var index = this.Index;
                if (index is object)
                {
                    return Linq.Expression.ArrayIndex(this.Array.LinqExpression, index.LinqExpression);
                }

                return Linq.Expression.ArrayIndex(this.Array.LinqExpression, this.Indexes.Select(i => i.LinqExpression));
            }
        }
    }
}
