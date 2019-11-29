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

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public string Method => Vocabulary.Method.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).Single();

        public IEnumerable<ExpressionNode> Arguments => Vocabulary.Arguments.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(Parse);

        public IEnumerable<TypeNode> TypeArguments => Vocabulary.TypeArguments.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(TypeNode.Parse);

        public override Expression Expression
        {
            get
            {
                var typeArguments = TypeArguments.Select(typeArg => typeArg.Type).ToArray();
                var arguments = Arguments.Select(arg => arg.Expression).ToArray();

                if (Instance is ExpressionNode instance)
                {
                    return Expression.Call(
                        instance.Expression,
                        Method,
                        typeArguments,
                        arguments
                    );
                }

                return Expression.Call(
                    Type.Type,
                    Method, 
                    typeArguments, 
                    arguments
                );
            }
        }
    }
}
