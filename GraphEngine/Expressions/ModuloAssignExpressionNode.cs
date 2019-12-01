namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class ModuloAssignExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public ModuloAssignExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.ModuloAssign;
    }
}