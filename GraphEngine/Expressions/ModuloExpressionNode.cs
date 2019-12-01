namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    internal class ModuloExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        public ModuloExpressionNode(INode node) : base(node) { }

        protected override ExpressionType BinaryType => ExpressionType.Modulo;
    }
}