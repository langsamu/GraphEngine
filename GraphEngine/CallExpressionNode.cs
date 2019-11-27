namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class CallExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal CallExpressionNode(INode node) : base(node) { }

        public ExpressionNode Instance => Vocabulary.Instance.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public string TypeName => Vocabulary.Type.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).SingleOrDefault();

        public string Method => Vocabulary.Method.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).Single();

        public IEnumerable<ExpressionNode> Arguments => Vocabulary.Arguments.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(Parse);

        public IEnumerable<string> TypeArguments => Vocabulary.TypeArguments.ObjectsOf(this).SelectMany(Graph.GetListItems).Cast<ILiteralNode>().Select(n => n.Value);

        public override Expression Expression
        {
            get
            {
                var argumentTypes = TypeArguments.Select(typeArg => Type.GetType(typeArg)).ToArray();
                var arguments = Arguments.Select(arg => arg.Expression).ToArray();

                if (Instance is ExpressionNode instance)
                {
                    return Expression.Call(
                        instance.Expression,
                        Method,
                        argumentTypes,
                        arguments
                    );
                }

                return Expression.Call(
                    Type.GetType(this.TypeName),
                    Method, 
                    argumentTypes, 
                    arguments
                );
            }
        }
    }
}
