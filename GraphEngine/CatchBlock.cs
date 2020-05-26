// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class CatchBlock : Node
    {
        [DebuggerStepThrough]
        internal CatchBlock(INode node)
            : base(node)
        {
        }

        public Type? Type
        {
            get => this.GetOptional(CatchType, Type.Parse);

            set => this.SetOptional(CatchType, value);
        }

        public Expression Body
        {
            get => this.GetRequired(CatchBody, Expression.Parse);

            set => this.SetRequired(CatchBody, value);
        }

        public Parameter? Variable
        {
            get => this.GetOptional(CatchVariable, Parameter.Parse);

            set => this.SetOptional(CatchVariable, value);
        }

        public Expression? Filter
        {
            get => this.GetOptional(CatchFilter, Expression.Parse);

            set => this.SetOptional(CatchFilter, value);
        }

        public Linq.CatchBlock LinqCatchBlock
        {
            get
            {
                var variable = this.Variable?.LinqParameter;
                return Linq.Expression.MakeCatchBlock(this.Type?.SystemType ?? variable?.Type, variable, this.Body.LinqExpression, this.Filter?.LinqExpression);
            }
        }

        internal static CatchBlock Parse(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return new CatchBlock(node);
        }
    }
}