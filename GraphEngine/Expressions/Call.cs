// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    // TODO: Add support for overloads with method
    public class Call : Expression
    {
        [DebuggerStepThrough]
        internal Call(INode node)
            : base(node)
        {
        }

        public Expression? Instance => Optional<Expression>(CallInstance);

        public Type? Type => Optional<Type>(CallType);

        public string Method => Required<string>(CallMethod);

        public IEnumerable<Expression> Arguments => List<Expression>(CallArguments);

        public IEnumerable<Type> TypeArguments => List<Type>(CallTypeArguments);

        public override Linq.Expression LinqExpression
        {
            get
            {
                var typeArguments = this.TypeArguments.Select(typeArg => typeArg.SystemType).ToArray();
                var arguments = this.Arguments.LinqExpressions().ToArray();

                if (this.Instance is Expression instance)
                {
                    return Linq.Expression.Call(
                        instance.LinqExpression,
                        this.Method,
                        typeArguments,
                        arguments);
                }

                if (this.Type is Type type)
                {
                    return Linq.Expression.Call(
                        type.SystemType,
                        this.Method,
                        typeArguments,
                        arguments);
                }

                throw new InvalidOperationException($"Missing both instance and type on call {this}");
            }
        }
    }
}
