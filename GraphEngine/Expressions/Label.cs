// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Label : Expression
    {
        [DebuggerStepThrough]
        internal Label(INode node)
            : base(node)
        {
        }

        public Target Target => Vocabulary.LabelTarget.ObjectsOf(this).Select(Target.Parse).Single();

        public Expression DefaultValue => Vocabulary.LabelDefaultValue.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public override Linq.Expression LinqExpression => Linq.Expression.Label(this.Target.LinqTarget, this.DefaultValue?.LinqExpression);
    }
}
