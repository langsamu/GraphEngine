namespace GraphEngine
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class VariableExpressionNode : ExpressionNode
    {
        public VariableExpressionNode(INode node) : base(node) { }

        public string TypeName => Vocabulary.Type.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).Single();

        public override Expression Expression => Expression.Variable(Type.GetType(TypeName));
    }
}