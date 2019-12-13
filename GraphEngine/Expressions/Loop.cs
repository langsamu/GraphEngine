// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    internal class Loop : Expression
    {
        [DebuggerStepThrough]
        public Loop(INode node)
            : base(node)
        {
        }

        public Expression Body => Vocabulary.LoopBody.ObjectsOf(this).Select(Parse).Single();

        public Target Break => Vocabulary.LoopBreak.ObjectsOf(this).Select(Target.Parse).SingleOrDefault();

        public Target Continue => Vocabulary.LoopContinue.ObjectsOf(this).Select(Target.Parse).SingleOrDefault();

        public override Linq.Expression LinqExpression
        {
            get
            {
                var body = this.Body;
                var @continue = this.Continue;
                var @break = this.Break;

                if (@continue is object)
                {
                    return Linq.Expression.Loop(body.LinqExpression, @break.LinqTarget, @continue.LinqTarget);
                }

                if (@break is object)
                {
                    return Linq.Expression.Loop(body.LinqExpression, @break.LinqTarget);
                }

                return Linq.Expression.Loop(body.LinqExpression);
            }
        }
    }
}