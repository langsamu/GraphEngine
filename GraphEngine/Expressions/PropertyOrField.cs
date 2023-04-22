// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using Linq = System.Linq.Expressions;

    public class PropertyOrField : MemberAccess
    {
        [DebuggerStepThrough]
        internal PropertyOrField(NodeWithGraph node)
            : base(node)
        {
            this.RdfType = Vocabulary.PropertyOrField;
        }

        public override Linq.Expression LinqExpression
        {
            get
            {
                if (this.Expression is Expression expression)
                {
                    return Linq.Expression.PropertyOrField(expression.LinqExpression, this.Name);
                }

                throw new InvalidOperationException($"Expression missing from node {this}");
            }
        }
    }
}
