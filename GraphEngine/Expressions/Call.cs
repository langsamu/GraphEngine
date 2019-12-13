// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Call : Expression
    {
        [DebuggerStepThrough]
        internal Call(INode node)
            : base(node)
        {
        }

        public Expression Instance => Vocabulary.CallInstance.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public Type Type => Vocabulary.CallType.ObjectsOf(this).Select(Type.Parse).SingleOrDefault();

        public string Method => Vocabulary.CallMethod.ObjectsOf(this).Cast<ILiteralNode>().Select(n => n.Value).Single();

        public IEnumerable<Expression> Arguments => Vocabulary.CallArguments.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public IEnumerable<Type> TypeArguments => Vocabulary.CallTypeArguments.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Type.Parse);

        public override Linq.Expression LinqExpression
        {
            get
            {
                var typeArguments = this.TypeArguments.Select(typeArg => typeArg.SystemType).ToArray();
                var arguments = this.Arguments.Select(arg => arg.LinqExpression).ToArray();

                if (this.Instance is Expression instance)
                {
                    return Linq.Expression.Call(
                        instance.LinqExpression,
                        this.Method,
                        typeArguments,
                        arguments);
                }

                return Linq.Expression.Call(
                    this.Type.SystemType,
                    this.Method,
                    typeArguments,
                    arguments);
            }
        }
    }
}
