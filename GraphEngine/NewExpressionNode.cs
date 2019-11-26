namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using VDS.RDF;
    using VDS.RDF.Nodes;

    public class NewExpressionNode : BinaryExpressionNode
    {
        [DebuggerStepThrough]
        internal NewExpressionNode(INode node) : base(node) { }

        public IEnumerable<ExpressionNode> Arguments
        {
            get
            {
                var argumentsNode = Vocabulary.Arguments.ObjectOf(this);

                if (argumentsNode is null)
                {
                    return null;
                }

                return Graph.GetListItems(argumentsNode).Select(Parse);
            }
        }

        public string TypeName => Vocabulary.Type.ObjectOf(this).AsValuedNode().AsString();

        public override Expression Expression
        {
            get
            {
                var arguments = Arguments;
                var type = Type.GetType(TypeName);

                if (arguments is null)
                {
                    return Expression.New(type);
                }

                var argumentExpressions = arguments.Select(arg => arg.Expression);
                var types = argumentExpressions.Select(expression => expression.Type).ToArray();
                var constructor = type.GetConstructor(types);

                return Expression.New(constructor, argumentExpressions);
            }
        }
    }
}
