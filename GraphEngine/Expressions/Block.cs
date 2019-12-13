// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Block : Expression
    {
        [DebuggerStepThrough]
        internal Block(INode node)
            : base(node)
        {
        }

        public IEnumerable<Expression> Expressions => List<Expression>(BlockExpressions);

        public IEnumerable<Parameter> Variables => List<Parameter>(BlockVariables);

        public override Linq.Expression LinqExpression => Linq.Expression.Block(this.Variables.Select(e => e.LinqParameter), this.Expressions.LinqExpressions());
    }
}
