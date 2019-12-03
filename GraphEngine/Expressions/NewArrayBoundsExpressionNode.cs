namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class NewArrayBoundsExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal NewArrayBoundsExpressionNode(INode node) : base(node) { }

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).Single();

        public IEnumerable<ExpressionNode> Bounds => Vocabulary.Bounds.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(Parse);

        public override Expression Expression => Expression.NewArrayBounds(Type.Type, Bounds.Select(b => b.Expression));
    }
}
