// MIT License, Copyright 2019 Samu Lang

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

        public Target Target => Optional<Target>(GotoTarget);

        public Type Type => Optional<Type>(GotoType);

        public Expression Value => Optional<Expression>(GotoValue);

        public override Linq.Expression LinqExpression => Linq.Expression.MakeGoto(this.Kind, this.Target?.LinqTarget, this.Value?.LinqExpression, this.Type?.SystemType);

        protected abstract Linq.GotoExpressionKind Kind { get; }
    }
}
