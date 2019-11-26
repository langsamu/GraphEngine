namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class LambdaExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal LambdaExpressionNode(INode node) : base(node) { }

        public ExpressionNode Body => ExpressionNode.Parse(Vocabulary.Body.ObjectOf(this));

        public override Expression Expression => Expression.Lambda(this.Body.Expression);
    }
}
