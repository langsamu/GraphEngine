// MIT License, Copyright 2020 Samu Lang

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

        public Expression Body
        {
            get => this.GetRequired(LoopBody, AsExpression);

            set => this.SetRequired(LoopBody, value);
        }

        public Target? Break
        {
            get => this.GetOptional(LoopBreak, AsTarget);

            set => this.SetOptional(LoopBreak, value);
        }

        public Target? Continue
        {
            get => this.GetOptional(LoopContinue, AsTarget);

            set => this.SetOptional(LoopContinue, value);
        }

        public override Linq.Expression LinqExpression
        {
            get
            {
                var body = this.Body;
                var @continue = this.Continue;
                var @break = this.Break;

                if (@break is object)
                {
                    if (@continue is object)
                    {
                        return Linq.Expression.Loop(body.LinqExpression, @break.LinqTarget, @continue.LinqTarget);
                    }

                    return Linq.Expression.Loop(body.LinqExpression, @break.LinqTarget);
                }

                return Linq.Expression.Loop(body.LinqExpression);
            }
        }
    }
}