// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class ArrayAccessExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal ArrayAccessExpressionNode(INode node)
            : base(node)
        {
        }

        public ExpressionNode Array => Vocabulary.ArrayAccessArray.ObjectsOf(this).Select(Parse).Single();

        public IEnumerable<ExpressionNode> Indexes => Vocabulary.ArrayAccessIndexes.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public override Expression Expression => Expression.ArrayAccess(this.Array.Expression, this.Indexes.Select(i => i.Expression));
    }
}
