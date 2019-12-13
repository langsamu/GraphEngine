// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class NewArrayBounds : Expression
    {
        [DebuggerStepThrough]
        internal NewArrayBounds(INode node)
            : base(node)
        {
        }

        public Type Type => Vocabulary.NewArrayBoundsType.ObjectsOf(this).Select(Type.Parse).Single();

        public IEnumerable<Expression> Bounds => Vocabulary.NewArrayBoundsBounds.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.NewArrayBounds(this.Type.SystemType, this.Bounds.Select(b => b.LinqExpression));
    }
}
