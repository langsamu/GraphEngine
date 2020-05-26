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

        public Expression Body
        {
            get => this.GetRequired(LambdaBody, Expression.Parse);

            set => this.SetRequired(LambdaBody, value);
        }

        public ICollection<Parameter> Parameters => this.Collection(LambdaParameters, Parameter.Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.Lambda(this.Body.LinqExpression, this.Parameters.Select(param => param.LinqParameter));
    }
}
