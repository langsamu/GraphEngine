// MIT License, Copyright 2020 Samu Lang

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

        public ICollection<Expression> Expressions => this.Collection<Expression>(BlockExpressions);

        public ICollection<Parameter> Variables => this.Collection<Parameter>(BlockVariables);

        public override Linq.Expression LinqExpression => Linq.Expression.Block(this.Variables.Select(e => e.LinqParameter), this.Expressions.LinqExpressions());

        public static Block Create(INode node) => new Block(node) { RdfType = Vocabulary.Block };
    }
}
