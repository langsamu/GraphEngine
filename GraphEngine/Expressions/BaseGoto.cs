// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public abstract class BaseGoto : Expression
    {
        [DebuggerStepThrough]
        public BaseGoto(INode node)
            : base(node)
        {
        }

        public Target Target => Vocabulary.GotoTarget.ObjectsOf(this).Select(Target.Parse).SingleOrDefault();

        public Type Type => Vocabulary.GotoType.ObjectsOf(this).Select(Type.Parse).SingleOrDefault();

        public Expression Value => Vocabulary.GotoValue.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public override Linq.Expression LinqExpression => Linq.Expression.MakeGoto(this.Kind, this.Target?.LinqTarget, this.Value?.LinqExpression, this.Type?.SystemType);

        protected abstract Linq.GotoExpressionKind Kind { get; }
    }
}
