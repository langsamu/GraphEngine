// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Default : Expression
    {
        [DebuggerStepThrough]
        internal Default(INode node)
            : base(node)
        {
        }

        public Type Type => Vocabulary.DefaultType.ObjectsOf(this).Select(Type.Parse).Single();

        public override Linq.Expression LinqExpression => Linq.Expression.Default(this.Type.SystemType);
    }
}
