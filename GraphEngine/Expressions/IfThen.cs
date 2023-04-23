// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using Linq = System.Linq.Expressions;

    public class IfThen : Condition
    {
        [DebuggerStepThrough]
        internal IfThen(NodeWithGraph node)
            : base(node)
            => this.RdfType = Vocabulary.IfThen;

        public override Linq.Expression LinqExpression => Linq.Expression.IfThen(this.Test.LinqExpression, this.IfTrue.LinqExpression);
    }
}
