// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Case : Node
    {
        [DebuggerStepThrough]
        internal Case(NodeWithGraph node)
            : base(node)
        {
        }

        public Expression Body
        {
            get => this.GetRequired(CaseBody, Expression.Parse);

            set => this.SetRequired(CaseBody, value);
        }

        public ICollection<Expression> TestValues => this.Collection(CaseTestValues, Expression.Parse);

        public Linq.SwitchCase LinqSwitchCase => Linq.Expression.SwitchCase(this.Body.LinqExpression, this.TestValues.LinqExpressions());

        internal static Case Parse(NodeWithGraph node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return new Case(node);
        }
    }
}
