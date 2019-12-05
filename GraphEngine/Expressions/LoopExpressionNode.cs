// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class LoopExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        public LoopExpressionNode(INode node)
            : base(node)
        {
        }

        public ExpressionNode Body => Vocabulary.LoopBody.ObjectsOf(this).Select(Parse).Single();

        public LabelTargetNode Break => Vocabulary.LoopBreak.ObjectsOf(this).Select(LabelTargetNode.Parse).SingleOrDefault();

        public LabelTargetNode Continue => Vocabulary.LoopContinue.ObjectsOf(this).Select(LabelTargetNode.Parse).SingleOrDefault();

        public override Expression Expression
        {
            get
            {
                var body = this.Body;
                var @continue = this.Continue;
                var @break = this.Break;

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