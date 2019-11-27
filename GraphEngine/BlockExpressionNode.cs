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
        
        public override Expression Expression => Expression.Block(Expressions.Select(e => e.Expression));
    }
}
