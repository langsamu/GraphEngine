// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Block : Expression
    {
        [DebuggerStepThrough]
        internal Block(NodeWithGraph node)
            : base(node)
        {
        }

        public Type? Type
        {
            get => this.GetOptional(BlockType, Type.Parse);

            set => this.SetOptional(BlockType, value);
        }

        public ICollection<Expression> Expressions => this.Collection(BlockExpressions, Expression.Parse);

        public ICollection<Parameter> Variables => this.Collection(BlockVariables, Parameter.Parse);

        public override Linq.Expression LinqExpression => (type: this.Type, empty: !this.Variables.Any()) switch
        {
            (type: not null, _) => Linq.Expression.Block(
                this.Type.SystemType,
                from e in this.Variables select e.LinqParameter,
                this.Expressions.LinqExpressions()),

            (_, empty: true) => Linq.Expression.Block(
                this.Expressions.LinqExpressions().ToArray()),

            _ => Linq.Expression.Block(
                from e in this.Variables select e.LinqParameter,
                this.Expressions.LinqExpressions()),
        };
    }
}
