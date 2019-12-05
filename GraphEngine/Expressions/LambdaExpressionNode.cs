// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class LambdaExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal LambdaExpressionNode(INode node)
            : base(node)
        {
        }

        public ExpressionNode Body => Vocabulary.LambdaBody.ObjectsOf(this).Select(Parse).Single();

        public IEnumerable<ParameterExpressionNode> Parameters => Vocabulary.LambdaParameters.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(ParameterExpressionNode.Parse);

        public override Expression Expression => Expression.Lambda(this.Body.Expression, this.Parameters.Select(param => param.Parameter));
    }
}
