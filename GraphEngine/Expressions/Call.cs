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

        public Expression? Instance
        {
            get => this.GetOptional(CallInstance, AsExpression);

            set => this.SetOptional(CallInstance, value);
        }

        public Type? Type
        {
            get => this.GetOptional(CallType, AsType);

            set => this.SetOptional(CallType, value);
        }

        public string Method
        {
            get => this.GetRequired(CallMethod, AsString);

            set => this.SetOptional(CallMethod, value);
        }

        public ICollection<Expression> Arguments => this.Collection(CallArguments, AsExpression);

        public ICollection<Type> TypeArguments => this.Collection(CallTypeArguments, AsType);

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
