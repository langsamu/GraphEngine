// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public abstract class Binary : Expression
    {
        [DebuggerStepThrough]
        protected Binary(INode node)
            : base(node)
        {
        }

        public Expression Left => Required<Expression>(BinaryLeft);

        public Expression Right => Required<Expression>(BinaryRight);

        public override Linq.Expression LinqExpression => Linq.Expression.MakeBinary(this.LinqBinaryType, this.Left.LinqExpression, this.Right.LinqExpression);

        protected abstract Linq.ExpressionType LinqBinaryType { get; }
    }
}
