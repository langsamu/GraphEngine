// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class ArrayIndex : Expression
    {
        [DebuggerStepThrough]
        internal ArrayIndex(INode node)
            : base(node)
        {
        }

        public Expression Array => Required<Expression>(ArrayIndexArray);

        public Expression? Index => Optional<Expression>(ArrayIndexIndex);

        public IEnumerable<Expression> Indexes => List<Expression>(ArrayIndexIndexes);

        public override Linq.Expression LinqExpression
        {
            get
            {
                var index = this.Index;
                if (index is object)
                {
                    return Linq.Expression.ArrayIndex(this.Array.LinqExpression, index.LinqExpression);
                }

                return Linq.Expression.ArrayIndex(this.Array.LinqExpression, this.Indexes.LinqExpressions());
            }
        }
    }
}
