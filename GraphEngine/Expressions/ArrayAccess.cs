// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class ArrayAccess : Expression
    {
        [DebuggerStepThrough]
        internal ArrayAccess(INode node)
            : base(node)
        {
        }

        public Expression Array
        {
            get => this.GetRequired<Expression>(ArrayAccessArray);

            set => this.SetRequired(ArrayAccessArray, value);
        }

        public ICollection<Expression> Indexes => this.Collection<Expression>(ArrayAccessIndexes);

        public override Linq.Expression LinqExpression => Linq.Expression.ArrayAccess(this.Array.LinqExpression, this.Indexes.LinqExpressions());
    }
}
