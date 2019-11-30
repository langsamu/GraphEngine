namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class BlockExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal BlockExpressionNode(INode node) : base(node) { }

        public IEnumerable<ExpressionNode> Expressions => Vocabulary.Expressions.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(Parse);

        public IEnumerable<ParameterExpressionNode> Variables => Vocabulary.Variables.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(v => new ParameterExpressionNode(v));

        public override Expression Expression => Expression.Block(Variables.Select(e => e.Parameter), Expressions.Select(e => e.Expression));
    }
}
