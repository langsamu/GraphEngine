// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Lambda : Expression
    {
        [DebuggerStepThrough]
        internal Lambda(INode node)
            : base(node)
        {
        }

        public Expression Body => Required<Expression>(LambdaBody);

        public IEnumerable<Parameter> Parameters => List<Parameter>(LambdaParameters);

        public override Linq.Expression LinqExpression => Linq.Expression.Lambda(this.Body.LinqExpression, this.Parameters.Select(param => param.LinqParameter));
    }
}
