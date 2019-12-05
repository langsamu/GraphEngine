// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class ConditionExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal ConditionExpressionNode(INode node)
            : base(node)
        {
        }

        public ExpressionNode Test => Vocabulary.ConditionTest.ObjectsOf(this).Select(Parse).Single();

        public ExpressionNode IfTrue => Vocabulary.ConditionIfTrue.ObjectsOf(this).Select(Parse).Single();

        public ExpressionNode IfFalse => Vocabulary.ConditionIfFalse.ObjectsOf(this).Select(Parse).Single();

        public TypeNode Type => Vocabulary.ConditionType.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public override Expression Expression
        {
            get
            {
                if (this.Type is TypeNode type)
                {
                    return Expression.Condition(this.Test.Expression, this.IfTrue.Expression, this.IfFalse.Expression, type.Type);
                }

                return Expression.Condition(this.Test.Expression, this.IfTrue.Expression, this.IfFalse.Expression);
            }
        }
    }
}
