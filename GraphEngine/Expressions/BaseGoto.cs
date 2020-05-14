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
            get => this.GetRequired<Target>(GotoTarget);

            set => this.SetRequired(GotoTarget, value);
        }

        public Type? Type
        {
            get => this.GetOptional<Type>(GotoType);

            set => this.SetOptional(GotoType, value);
        }

        public Expression? Value
        {
            get => this.GetOptional<Expression>(GotoValue);

            set => this.SetOptional(GotoValue, value);
        }

        public override Linq.Expression LinqExpression => Linq.Expression.MakeGoto(this.Kind, this.Target.LinqTarget, this.Value?.LinqExpression, this.Type?.SystemType ?? typeof(void));

        protected abstract Linq.GotoExpressionKind Kind { get; }
    }
}
