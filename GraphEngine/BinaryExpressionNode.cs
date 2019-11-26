namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;

    public abstract class BinaryExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        protected BinaryExpressionNode(INode node) : base(node) { }

        public ExpressionNode Left => Parse(Vocabulary.Left.ObjectOf(this));

        public ExpressionNode Right => Parse(Vocabulary.Right.ObjectOf(this));
    }
}
