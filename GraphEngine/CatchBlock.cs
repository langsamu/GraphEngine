// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
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
            get => this.GetOptional<Type>(CatchType);

            set => this.SetOptional(CatchType, value);
        }

        public Expression Body
        {
            get => this.GetRequired<Expression>(CatchBody);

            set => this.SetRequired(CatchBody, value);
        }

        public Parameter? Variable
        {
            get => this.GetOptional<Parameter>(CatchVariable);

            set => this.SetOptional(CatchVariable, value);
        }

        public Expression? Filter
        {
            get => this.GetOptional<Expression>(CatchFilter);

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
    }
}