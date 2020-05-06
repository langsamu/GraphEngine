// MIT License, Copyright 2019 Samu Lang

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

        public Expression Operand => Required<Expression>(UnaryOperand);

        public Type? Type => Optional<Type>(UnaryType);

        public override Linq.Expression LinqExpression => Linq.Expression.MakeUnary(this.LinqUnaryType, this.Operand.LinqExpression, this.Type?.SystemType);

        protected abstract Linq.ExpressionType LinqUnaryType { get; }
    }
}
