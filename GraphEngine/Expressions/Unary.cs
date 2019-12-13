// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public abstract class Unary : Expression
    {
        [DebuggerStepThrough]
        protected Unary(INode node)
            : base(node)
        {
        }

        public Expression Operand => Vocabulary.UnaryOperand.ObjectsOf(this).Select(Parse).Single();

        public Type Type => Vocabulary.UnaryType.ObjectsOf(this).Select(Type.Parse).SingleOrDefault();

        public override Linq.Expression LinqExpression => Linq.Expression.MakeUnary(this.UnaryType, this.Operand.LinqExpression, this.Type?.SystemType);

        protected abstract Linq.ExpressionType UnaryType { get; }
    }
}
