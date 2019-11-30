namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class ConditionExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal ConditionExpressionNode(INode node) : base(node) { }

        public ExpressionNode Test => Vocabulary.Test.ObjectsOf(this).Select(Parse).Single();

        public ExpressionNode IfTrue => Vocabulary.IfTrue.ObjectsOf(this).Select(Parse).Single();

        public ExpressionNode IfFalse => Vocabulary.IfFalse.ObjectsOf(this).Select(Parse).Single();

        public override Expression Expression => Expression.Condition(Test.Expression, IfTrue.Expression, IfFalse.Expression);
    }
}
