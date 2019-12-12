// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class EmptyExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal EmptyExpressionNode(INode node)
            : base(node)
        {
        }

        public override Expression Expression => Expression.Empty();
    }
}
