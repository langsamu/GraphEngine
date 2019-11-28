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

        // TODO: Handle non-expressions, e.g. uri node = System.Uri, blank node = new object()
        public static ExpressionNode Parse(INode node)
        {
            var nodeType = node.NodeType;
            switch (nodeType)
            {
                case NodeType.Blank:
                case NodeType.Uri:
                    return ParseResource(node);

                default:
                    throw new Exception($"unknown node type {nodeType} on node {node}");
            }
        }

        private static ExpressionNode ParseResource(INode node)
        {
            var type = Vocabulary.RdfType.ObjectOf(node);
            switch (type)
            {
                case INode t when t.Equals(Vocabulary.Add): return new AddExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Subtract): return new SubtractExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Block): return new BlockExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Lambda): return new LambdaExpressionNode(node);
                case INode t when t.Equals(Vocabulary.New): return new NewExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Assign): return new AssignExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Variable): return new VariableExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Call): return new CallExpressionNode(node);
                case INode t when t.Equals(Vocabulary.Constant): return new ConstantExpressionNode(node);

                default: throw new Exception($"unknown type {type} on node {node}");
            }
        }
    }
}
