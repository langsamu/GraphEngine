// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public abstract class BaseGoto : Expression
    {
        [DebuggerStepThrough]
        public BaseGoto(INode node)
            : base(node)
        {
        }

        public Target Target
        {
            get => this.GetRequired(GotoTarget, Target.Parse);

            set => this.SetRequired(GotoTarget, value);
        }

        public Type? Type
        {
            get => this.GetOptional(GotoType, Type.Parse);

            set => this.SetOptional(GotoType, value);
        }

        public Expression? Value
        {
            get => this.GetOptional(GotoValue, Expression.Parse);

            set => this.SetOptional(GotoValue, value);
        }

        public override Linq.Expression LinqExpression => Linq.Expression.MakeGoto(this.Kind, this.Target.LinqTarget, this.Value?.LinqExpression, this.Type?.SystemType ?? typeof(void));

        protected abstract Linq.GotoExpressionKind Kind { get; }

        public static BaseGoto Create(INode node, Linq.GotoExpressionKind kind)
        {
            return kind switch
            {
                Linq.GotoExpressionKind.Goto => new Goto(node),
                Linq.GotoExpressionKind.Return => new Return(node),
                Linq.GotoExpressionKind.Break => new Break(node),
                Linq.GotoExpressionKind.Continue => new Continue(node),

                _ => throw new System.Exception("{type} is not goto")
            };
        }
    }
}
