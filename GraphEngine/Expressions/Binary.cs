// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public abstract class Binary : Expression
    {
        [DebuggerStepThrough]
        protected Binary(INode node)
            : base(node)
        {
        }

        public Expression Left => Vocabulary.BinaryLeft.ObjectsOf(this).Select(Parse).Single();

        public Expression Right => Vocabulary.BinaryRight.ObjectsOf(this).Select(Parse).Single();

        public override Linq.Expression LinqExpression => Linq.Expression.MakeBinary(this.BinaryType, this.Left.LinqExpression, this.Right.LinqExpression);

        protected abstract Linq.ExpressionType BinaryType { get; }
    }
}
