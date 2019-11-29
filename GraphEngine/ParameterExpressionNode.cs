namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class ParameterExpressionNode : ExpressionNode
    {
        private static IDictionary<INode, ParameterExpression> cache = new Dictionary<INode, ParameterExpression>();

        [DebuggerStepThrough]
        public ParameterExpressionNode(INode node) : base(node) { }

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).Single();

        public override Expression Expression => Parameter;

        public ParameterExpression Parameter
        {
            get
            {
                if (!cache.TryGetValue(this, out var param))
                {
                    param = cache[this] = Expression.Parameter(Type.Type);
                }

                return param;
            }
        }
    }
}