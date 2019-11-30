namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public abstract class BinaryExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        protected BinaryExpressionNode(INode node) : base(node) { }

        public ExpressionNode Left => Vocabulary.Left.ObjectsOf(this).Select(Parse).Single();

        public ExpressionNode Right => Vocabulary.Right.ObjectsOf(this).Select(Parse).Single();

        protected abstract ExpressionType Type { get; }

        public override Expression Expression => Expression.MakeBinary(Type, Left.Expression, Right.Expression);
    }
}
