// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Block : Expression
    {
        [DebuggerStepThrough]
        internal Block(INode node)
            : base(node)
        {
        }

        public IEnumerable<Expression> Expressions => Vocabulary.BlockExpressions.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public IEnumerable<Parameter> Variables => Vocabulary.BlockVariables.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parameter.Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.Block(this.Variables.Select(e => e.LinqParameter), this.Expressions.Select(e => e.LinqExpression));
    }
}
