namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class LambdaExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal LambdaExpressionNode(INode node) : base(node) { }

        public ExpressionNode Body => Parse(Vocabulary.Body.ObjectOf(this));

        public override Expression Expression => Expression.Lambda(Body.Expression);
    }
}
