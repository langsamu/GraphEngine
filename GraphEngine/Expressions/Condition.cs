﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Condition : Expression
    {
        [DebuggerStepThrough]
        internal Condition(NodeWithGraph node)
            : base(node)
        {
        }

        public Expression Test
        {
            get => this.GetRequired(ConditionTest, Expression.Parse);

            set => this.SetRequired(ConditionTest, value);
        }

        public Expression IfTrue
        {
            get => this.GetRequired(ConditionIfTrue, Expression.Parse);

            set => this.SetRequired(ConditionIfTrue, value);
        }

        public Expression IfFalse
        {
            get => this.GetRequired(ConditionIfFalse, Expression.Parse);

            set => this.SetRequired(ConditionIfFalse, value);
        }

        public Type? Type
        {
            get => this.GetOptional(ConditionType, Type.Parse);

            set => this.SetOptional(ConditionType, value);
        }

        public override Linq.Expression LinqExpression => this.Type switch
        {
            Type type => Linq.Expression.Condition(this.Test.LinqExpression, this.IfTrue.LinqExpression, this.IfFalse.LinqExpression, type.SystemType),
            _ => Linq.Expression.Condition(this.Test.LinqExpression, this.IfTrue.LinqExpression, this.IfFalse.LinqExpression)
        };
    }
}
