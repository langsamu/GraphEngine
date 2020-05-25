// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Case : Node
    {
        [DebuggerStepThrough]
        internal Case(INode node)
            : base(node)
        {
        }

        public Expression Body
        {
            get => this.GetRequired(CaseBody, AsExpression);

            set => this.SetRequired(CaseBody, value);
        }

        public ICollection<Expression> TestValues => this.Collection(CaseTestValues, AsExpression);

        public Linq.SwitchCase LinqSwitchCase => Linq.Expression.SwitchCase(this.Body.LinqExpression, this.TestValues.LinqExpressions());
    }
}
