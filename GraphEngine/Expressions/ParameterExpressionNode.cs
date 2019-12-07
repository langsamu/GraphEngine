// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    // TODO: Add name
    public class ParameterExpressionNode : ExpressionNode
    {
        private static readonly IDictionary<INode, ParameterExpression> Cache = new Dictionary<INode, ParameterExpression>();

        [DebuggerStepThrough]
        public ParameterExpressionNode(INode node)
            : base(node)
        {
        }

        public TypeNode Type => Vocabulary.ParameterType.ObjectsOf(this).Select(TypeNode.Parse).Single();

        public override Expression Expression => this.Parameter;

        public ParameterExpression Parameter
        {
            get
            {
                if (!Cache.TryGetValue(this, out var param))
                {
                    param = Cache[this] = Expression.Parameter(this.Type.Type);
                }

                return param;
            }
        }

        internal static new ParameterExpressionNode Parse(INode node) => new ParameterExpressionNode(node);
    }
}