// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Condition : Expression
    {
        [DebuggerStepThrough]
        internal Condition(INode node)
            : base(node)
        {
        }

        public Expression Test => Vocabulary.ConditionTest.ObjectsOf(this).Select(Parse).Single();

        public Expression IfTrue => Vocabulary.ConditionIfTrue.ObjectsOf(this).Select(Parse).Single();

        public Expression IfFalse => Vocabulary.ConditionIfFalse.ObjectsOf(this).Select(Parse).Single();

        public Type Type => Vocabulary.ConditionType.ObjectsOf(this).Select(Type.Parse).SingleOrDefault();

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
