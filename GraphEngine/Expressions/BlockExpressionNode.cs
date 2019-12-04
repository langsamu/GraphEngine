// MIT License, Copyright 2019 Samu Lang

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
        internal BlockExpressionNode(INode node)
            : base(node)
        {
        }

        public IEnumerable<ExpressionNode> Expressions => Vocabulary.Expressions.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public IEnumerable<ParameterExpressionNode> Variables => Vocabulary.Variables.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(ParameterExpressionNode.Parse);

        public override Expression Expression => Expression.Block(this.Variables.Select(e => e.Parameter), this.Expressions.Select(e => e.Expression));
    }
}
