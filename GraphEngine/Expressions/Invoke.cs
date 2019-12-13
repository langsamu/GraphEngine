// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Invoke : Expression
    {
        [DebuggerStepThrough]
        internal Invoke(INode node)
            : base(node)
        {
        }

        public Expression ExpressionNode => Vocabulary.InvokeExpression.ObjectsOf(this).Select(Parse).Single();

        public IEnumerable<Expression> Arguments => Vocabulary.InvokeArguments.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.Invoke(this.ExpressionNode.LinqExpression, this.Arguments.Select(arg => arg.LinqExpression));
    }
}
