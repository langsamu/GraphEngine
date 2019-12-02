namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public abstract class UnaryExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        protected UnaryExpressionNode(INode node) : base(node) { }

        public ExpressionNode Operand => Vocabulary.Operand.ObjectsOf(this).Select(Parse).Single();

        protected abstract ExpressionType UnaryType { get; }

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public override Expression Expression => Expression.MakeUnary(UnaryType, Operand.Expression, Type?.Type);
    }
}
