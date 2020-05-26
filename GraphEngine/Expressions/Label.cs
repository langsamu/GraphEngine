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

        public Target Target
        {
            get => this.GetRequired(LabelTarget, Target.Parse);

            set => this.SetRequired(LabelTarget, value);
        }

        public Expression? DefaultValue
        {
            get => this.GetOptional(LabelDefaultValue, Expression.Parse);

            set => this.SetOptional(LabelDefaultValue, value);
        }

        public override Linq.Expression LinqExpression => Linq.Expression.Label(this.Target.LinqTarget, this.DefaultValue?.LinqExpression);
    }
}
