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

        public IEnumerable<ExpressionNode> Expressions => this.Graph.GetListItems(Vocabulary.Expressions.ObjectOf(this)).Select(ExpressionNode.Parse);

        public override Expression Expression => Expression.Block(this.Expressions.Select(e => e.Expression));
    }
}
