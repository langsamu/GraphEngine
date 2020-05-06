// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Label : Expression
    {
        [DebuggerStepThrough]
        internal Label(INode node)
            : base(node)
        {
        }

        public Target Target => Required<Target>(LabelTarget);

        public Expression? DefaultValue => Optional<Expression>(LabelDefaultValue);

        public override Linq.Expression LinqExpression => Linq.Expression.Label(this.Target.LinqTarget, this.DefaultValue?.LinqExpression);
    }
}
