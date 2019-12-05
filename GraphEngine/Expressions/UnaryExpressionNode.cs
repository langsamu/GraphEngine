// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public abstract class UnaryExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        protected UnaryExpressionNode(INode node)
            : base(node)
        {
        }

        public ExpressionNode Operand => Vocabulary.UnaryOperand.ObjectsOf(this).Select(Parse).Single();

        public TypeNode Type => Vocabulary.UnaryType.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public override Expression Expression => Expression.MakeUnary(this.UnaryType, this.Operand.Expression, this.Type?.Type);

        protected abstract ExpressionType UnaryType { get; }
    }
}
