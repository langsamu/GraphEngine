namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class InvokeExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal InvokeExpressionNode(INode node) : base(node) { }

        public ExpressionNode ExpressionNode => Vocabulary.Expression.ObjectsOf(this).Select(Parse).Single();

        public IEnumerable<ExpressionNode> Arguments => Vocabulary.Arguments.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(Parse);

        public override Expression Expression => Expression.Invoke(ExpressionNode.Expression, Arguments.Select(arg => arg.Expression));
    }
}
