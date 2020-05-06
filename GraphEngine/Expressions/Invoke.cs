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

        public Expression ExpressionNode => Required<Expression>(InvokeExpression);

        public IEnumerable<Expression> Arguments => List<Expression>(InvokeArguments);

        public override Linq.Expression LinqExpression => Linq.Expression.Invoke(this.ExpressionNode.LinqExpression, this.Arguments.LinqExpressions());
    }
}
