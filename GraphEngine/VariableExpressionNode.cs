namespace GraphEngine
{
    using System;
    using System.Linq.Expressions;
    using VDS.RDF;
    using VDS.RDF.Nodes;

    internal class VariableExpressionNode : ExpressionNode
    {
        public VariableExpressionNode(INode node) : base(node) { }

        public string TypeName => Vocabulary.Type.ObjectOf(this).AsValuedNode().AsString();
 
        public override Expression Expression => Expression.Variable(Type.GetType(TypeName));
    }
}