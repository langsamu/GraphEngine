namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class LoopExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        public LoopExpressionNode(INode node) : base(node) { }

        public ExpressionNode Body => Vocabulary.Body.ObjectsOf(this).Select(Parse).Single();

        public LabelTargetNode Break => Vocabulary.BreakLabel.ObjectsOf(this).Select(LabelTargetNode.Parse).SingleOrDefault();

        public LabelTargetNode Continue => Vocabulary.ContinueLabel.ObjectsOf(this).Select(LabelTargetNode.Parse).SingleOrDefault();

        public override Expression Expression
        {
            get
            {
                var body = Body;
                var @continue = Continue;
                var @break = Break;

                if (@continue is object)
                {
                    return Expression.Loop(body.Expression, @break.LabelTarget, @continue.LabelTarget);
                }

                if (@break is object)
                {
                    return Expression.Loop(body.Expression, @break.LabelTarget);
                }

                return Expression.Loop(body.Expression);
            }
        }
    }
}