// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class ReferenceNotEqual : NotEqual
    {
        [DebuggerStepThrough]
        internal ReferenceNotEqual(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.ReferenceNotEqual;
        }

        public override Linq.Expression LinqExpression => Linq.Expression.ReferenceNotEqual(this.Left.LinqExpression, this.Right.LinqExpression);
    }
}
