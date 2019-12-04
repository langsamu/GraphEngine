// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class NewExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal NewExpressionNode(INode node)
            : base(node)
        {
        }

        public IEnumerable<ExpressionNode> Arguments => Vocabulary.Arguments.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).Single();

        public override Expression Expression
        {
            get
            {
                var argumentExpressions = this.Arguments.Select(arg => arg.Expression);
                var types = argumentExpressions.Select(expression => expression.Type).ToArray();
                var constructor = this.Type.Type.GetConstructor(types);

                return Expression.New(constructor, argumentExpressions);
            }
        }
    }
}
