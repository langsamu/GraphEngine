// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Condition : Expression
    {
        [DebuggerStepThrough]
        internal Condition(INode node)
            : base(node)
        {
        }

        public Expression Test
        {
            get => this.GetRequired<Expression>(ConditionTest);

            set => this.SetRequired(ConditionTest, value);
        }

        public Expression IfTrue
        {
            get => this.GetRequired<Expression>(ConditionIfTrue);

            set => this.SetRequired(ConditionIfTrue, value);
        }

        public Expression IfFalse
        {
            get => this.GetRequired<Expression>(ConditionIfFalse);

            set => this.SetRequired(ConditionIfFalse, value);
        }

        public Type? Type
        {
            get => this.GetOptional<Type>(ConditionType);

            set => this.SetOptional(ConditionType, value);
        }

        public override Linq.Expression LinqExpression
        {
            get
            {
                if (this.Type is Type type)
                {
                    return Linq.Expression.Condition(this.Test.LinqExpression, this.IfTrue.LinqExpression, this.IfFalse.LinqExpression, type.SystemType);
                }

                return Linq.Expression.Condition(this.Test.LinqExpression, this.IfTrue.LinqExpression, this.IfFalse.LinqExpression);
            }
        }

        public static Condition Create(INode node) => new Condition(node) { RdfType = Vocabulary.Condition };
    }
}
