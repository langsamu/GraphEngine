// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public abstract class BinaryExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        protected BinaryExpressionNode(INode node)
            : base(node)
        {
        }

        public ExpressionNode Left => Vocabulary.BinaryLeft.ObjectsOf(this).Select(Parse).Single();

        public ExpressionNode Right => Vocabulary.BinaryRight.ObjectsOf(this).Select(Parse).Single();

        public override Expression Expression => Expression.MakeBinary(this.BinaryType, this.Left.Expression, this.Right.Expression);

        protected abstract ExpressionType BinaryType { get; }
    }
}
