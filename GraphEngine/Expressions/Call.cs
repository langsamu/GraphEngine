// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Call : Expression
    {
        [DebuggerStepThrough]
        internal Call(INode node)
            : base(node)
        {
        }

        public Expression? Instance
        {
            get => this.GetOptional(CallInstance, Expression.Parse);

            set => this.SetOptional(CallInstance, value);
        }

        public Type? Type
        {
            get => this.GetOptional(CallType, Type.Parse);

            set => this.SetOptional(CallType, value);
        }

        public Method? Method
        {
            get => this.GetOptional(CallMethod, Method.Parse);

            set => this.SetOptional(CallMethod, value);
        }

        public string? MethodName
        {
            get => this.GetOptional(CallMethodName, AsString);

            set => this.SetOptional(CallMethodName, value);
        }

        public ICollection<Expression> Arguments => this.Collection(CallArguments, Expression.Parse);

        public ICollection<Type> TypeArguments => this.Collection(CallTypeArguments, Type.Parse);

        public override Linq.Expression LinqExpression
        {
            get
            {
                if (this.Method is Method method)
                {
                    return Linq.Expression.Call(
                       this.Instance?.LinqExpression,
                       method.ReflectionMethod,
                       this.Arguments.LinqExpressions());
                }
                else if (this.Type is Type type)
                {
                    return Linq.Expression.Call(
                        type.SystemType,
                        this.MethodName,
                        this.TypeArguments.Select(typeArg => typeArg.SystemType).ToArray(),
                        this.Arguments.LinqExpressions().ToArray());
                }
                else
                {
                    return Linq.Expression.Call(
                        this.Instance?.LinqExpression,
                        this.MethodName,
                        this.TypeArguments.Select(typeArg => typeArg.SystemType).ToArray(),
                        this.Arguments.LinqExpressions().ToArray());
                }
            }
        }
    }
}
