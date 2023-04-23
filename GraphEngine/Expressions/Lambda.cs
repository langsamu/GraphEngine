// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Lambda : Expression
    {
        [DebuggerStepThrough]
        internal Lambda(NodeWithGraph node)
            : base(node)
        {
        }

        public Expression Body
        {
            get => this.GetRequired(LambdaBody, Expression.Parse);

            set => this.SetRequired(LambdaBody, value);
        }

        public ICollection<Parameter> Parameters => this.Collection(LambdaParameters, Parameter.Parse);

        public override Linq.Expression LinqExpression => this.LinqLambda;

        public LambdaExpression LinqLambda =>
            Linq.Expression.Lambda(
                this.Body.LinqExpression,
                from param in this.Parameters select param.LinqParameter);

        internal static new Lambda Parse(NodeWithGraph node) => node switch
        {
            null => throw new ArgumentNullException(nameof(node)),
            _ => new Lambda(node)
        };
    }
}
