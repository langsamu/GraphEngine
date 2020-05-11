// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class IfThen : Condition
    {
        [DebuggerStepThrough]
        internal IfThen(INode node)
            : base(node)
        {
        }

        public override Linq.Expression LinqExpression => Linq.Expression.IfThen(this.Test.LinqExpression, this.IfTrue.LinqExpression);

        public static IfThen Create(INode node) => new IfThen(node) { RdfType = Vocabulary.IfThen };
    }
}
