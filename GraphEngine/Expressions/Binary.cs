// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
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

            set => this.SetRequired(BinaryLeft, value);
        }

        public Expression Right
        {
            get => this.GetRequired<Expression>(BinaryRight);

            set => this.SetRequired(BinaryRight, value);
        }

        public override Linq.Expression LinqExpression => Linq.Expression.MakeBinary(this.LinqBinaryType, this.Left.LinqExpression, this.Right.LinqExpression);

        protected abstract Linq.ExpressionType LinqBinaryType { get; }

        public static Binary Create(INode node, Linq.ExpressionType type)
        {
            return type switch
            {
                Linq.ExpressionType.Add => new Add(node),
                Linq.ExpressionType.Assign => new Assign(node),
                Linq.ExpressionType.GreaterThan => new GreaterThan(node),
                Linq.ExpressionType.MultiplyAssign => new MultiplyAssign(node),

                _ => throw new Exception("{type} is not binary")
            };
        }
    }
}
