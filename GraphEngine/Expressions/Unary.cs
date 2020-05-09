// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public abstract class Unary : Expression
    {
        [DebuggerStepThrough]
        protected Unary(INode node)
            : base(node)
        {
        }

        public Expression Operand
        {
            get => this.GetRequired<Expression>(UnaryOperand);

            set => this.SetRequired(UnaryOperand, value);
        }

        public Type? Type
        {
            get => this.GetOptional<Type>(UnaryType);

            set => this.SetOptional(UnaryType, value);
        }

        public override Linq.Expression LinqExpression => Linq.Expression.MakeUnary(this.LinqUnaryType, this.Operand.LinqExpression, this.Type?.SystemType);

        protected abstract Linq.ExpressionType LinqUnaryType { get; }
    }
}
