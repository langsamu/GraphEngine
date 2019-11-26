namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public abstract class ExpressionNode : WrapperNode
    {
        [DebuggerStepThrough]
        protected ExpressionNode(INode node) : base(node) { }

        public abstract Expression Expression { get; }

        public static ExpressionNode Parse(INode node)
        {
            switch (node.NodeType)
            {
                case NodeType.Blank:
                case NodeType.Uri:
                    return ParseResource(node);

                case NodeType.Literal:
                    return new ConstantExpressionNode(node);

                default:
                    throw new Exception("unknown node type");
            }
        }

        private static ExpressionNode ParseResource(INode node)
        {
            switch (Vocabulary.RdfType.ObjectOf(node))
            {
                case INode t when t.Equals(Vocabulary.Add): return new AddExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Subtract): return new SubtractExpressionNode(node);

                default: throw new Exception("unknown expression node type");
            }
        }
    }
}
