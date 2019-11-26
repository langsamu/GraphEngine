namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;
    using VDS.RDF.Nodes;

    public class ConstantExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal ConstantExpressionNode(INode node) : base(node) { }

        private object Value => this.AsValuedNode().AsInteger();

        public override Expression Expression => Expression.Constant(this.Value);
    }
}
