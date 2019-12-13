// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Lambda : Expression
    {
        [DebuggerStepThrough]
        internal Lambda(INode node)
            : base(node)
        {
        }

        public Expression Body => Vocabulary.LambdaBody.ObjectsOf(this).Select(Parse).Single();

        public IEnumerable<Parameter> Parameters => Vocabulary.LambdaParameters.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parameter.Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.Lambda(this.Body.LinqExpression, this.Parameters.Select(param => param.LinqParameter));
    }
}
