// MIT License, Copyright 2020 Samu Lang

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

        public Expression Left
        {
            get => this.GetRequired<Expression>(BinaryLeft);

            set => this.SetRequired(BinaryLeft, GotoValue);
        }

        public Expression Right
        {
            get => this.GetRequired<Expression>(BinaryRight);

            set => this.SetRequired(BinaryRight, value);
        }

        public override Linq.Expression LinqExpression => Linq.Expression.MakeBinary(this.LinqBinaryType, this.Left.LinqExpression, this.Right.LinqExpression);

        protected abstract Linq.ExpressionType LinqBinaryType { get; }
    }
}
