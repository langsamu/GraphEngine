// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class ReferenceEqual : Equal
    {
        [DebuggerStepThrough]
        internal ReferenceEqual(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.ReferenceEqual;
        }

        public override Linq.Expression LinqExpression => Linq.Expression.ReferenceEqual(this.Left.LinqExpression, this.Right.LinqExpression);
    }
}
