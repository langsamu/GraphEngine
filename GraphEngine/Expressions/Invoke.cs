// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Invoke : Expression
    {
        [DebuggerStepThrough]
        internal Invoke(INode node)
            : base(node)
        {
        }

        public Expression Expression
        {
            get => this.GetRequired(InvokeExpression, Expression.Parse);

            set => this.SetRequired(InvokeExpression, value);
        }

        public ICollection<Expression> Arguments => this.Collection(InvokeArguments, Expression.Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.Invoke(this.Expression.LinqExpression, this.Arguments.LinqExpressions());
    }
}
