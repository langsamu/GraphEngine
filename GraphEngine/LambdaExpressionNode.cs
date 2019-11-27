namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class LambdaExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal LambdaExpressionNode(INode node) : base(node) { }

        public ExpressionNode Body => Vocabulary.Body.ObjectsOf(this).Select(Parse).Single();

        public override Expression Expression => Expression.Lambda(Body.Expression);
    }
}
