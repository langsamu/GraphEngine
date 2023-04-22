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

        public override Linq.Expression LinqExpression
        {
            get
            {
                if (this.Type is Type type)
                {
                    return Linq.Expression.Block(
                        type.SystemType,
                        this.Variables.Select(e => e.LinqParameter),
                        this.Expressions.LinqExpressions());
                }

                var variables = this.Variables;

                if (!variables.Any())
                {
                    return Linq.Expression.Block(
                        this.Expressions.LinqExpressions().ToArray());
                }

                return Linq.Expression.Block(
                    variables.Select(e => e.LinqParameter),
                    this.Expressions.LinqExpressions());
            }
        }
    }
}
