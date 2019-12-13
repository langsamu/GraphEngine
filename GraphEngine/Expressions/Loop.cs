// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    internal class Loop : Expression
    {
        [DebuggerStepThrough]
        public Loop(INode node)
            : base(node)
        {
        }

        public Expression Body => Required<Expression>(LoopBody);

        public Target Break => Optional<Target>(LoopBreak);

        public Target Continue => Optional<Target>(LoopContinue);

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