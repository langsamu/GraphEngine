// MIT License, Copyright 2019 Samu Lang

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

        public Type? Type => Optional<Type>(CatchType);

        public Expression Body => Required<Expression>(CatchBody);

        public Parameter? Variable => Optional<Parameter>(CatchVariable);

        public Expression? Filter => Optional<Expression>(CatchFilter);

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