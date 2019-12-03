namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class LambdaExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal LambdaExpressionNode(INode node) : base(node) { }

        public ExpressionNode Body => Vocabulary.Body.ObjectsOf(this).Select(Parse).Single();

        public IEnumerable<ParameterExpressionNode> Parameters => Vocabulary.Parameters.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(ParameterExpressionNode.Parse);

        public override Expression Expression => Expression.Lambda(Body.Expression, Parameters.Select(param => param.Parameter));
    }
}
