// MIT License, Copyright 2019 Samu Lang

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

        public Expression Test => Required<Expression>(ConditionTest);

        public Expression IfTrue => Required<Expression>(ConditionIfTrue);

        public Expression IfFalse => Required<Expression>(ConditionIfFalse);

        public Type? Type => Optional<Type>(ConditionType);

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
    }
}
