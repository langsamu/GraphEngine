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
                    return new AddExpressionNode(node);

                case NodeType.Literal:
                    return new ConstantExpressionNode(node);

                default:
                    throw new Exception("unknown node type");
            }
        }
    }
}
