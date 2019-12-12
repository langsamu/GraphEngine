// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class ArrayIndexExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal ArrayIndexExpressionNode(INode node)
            : base(node)
        {
        }

        public ExpressionNode Array => Vocabulary.ArrayIndexArray.ObjectsOf(this).Select(Parse).Single();

        public ExpressionNode Index => Vocabulary.ArrayIndexIndex.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public IEnumerable<ExpressionNode> Indexes => Vocabulary.ArrayIndexIndexes.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public override Expression Expression
        {
            get
            {
                var index = this.Index;
                if (index is object)
                {
                    return Expression.ArrayIndex(this.Array.Expression, index.Expression);
                }

                return Expression.ArrayIndex(this.Array.Expression, this.Indexes.Select(i => i.Expression));
            }
        }
    }
}
