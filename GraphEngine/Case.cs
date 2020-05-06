// MIT License, Copyright 2019 Samu Lang

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

        public Expression Body => Required<Expression>(CaseBody);

        public IEnumerable<Expression> TestValues => List<Expression>(CaseTestValues);

        public Linq.SwitchCase LinqSwitchCase => Linq.Expression.SwitchCase(this.Body.LinqExpression, this.TestValues.LinqExpressions());
    }
}
