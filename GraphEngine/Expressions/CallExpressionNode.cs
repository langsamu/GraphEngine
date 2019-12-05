// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class CallExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal CallExpressionNode(INode node)
            : base(node)
        {
        }

        public ExpressionNode Instance => Vocabulary.CallInstance.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public TypeNode Type => Vocabulary.CallType.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public string Method => Vocabulary.CallMethod.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).Single();

        public IEnumerable<ExpressionNode> Arguments => Vocabulary.CallArguments.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public IEnumerable<TypeNode> TypeArguments => Vocabulary.CallTypeArguments.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(TypeNode.Parse);

        public override Expression Expression
        {
            get
            {
                var typeArguments = this.TypeArguments.Select(typeArg => typeArg.Type).ToArray();
                var arguments = this.Arguments.Select(arg => arg.Expression).ToArray();

                if (this.Instance is ExpressionNode instance)
                {
                    return Expression.Call(
                        instance.Expression,
                        this.Method,
                        typeArguments,
                        arguments);
                }

                return Expression.Call(
                    this.Type.Type,
                    this.Method,
                    typeArguments,
                    arguments);
            }
        }
    }
}
