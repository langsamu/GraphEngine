// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Try : Expression
    {
        [DebuggerStepThrough]
        internal Try(NodeWithGraph node)
            : base(node)
        {
        }

        public Type? Type
        {
            get => this.GetOptional(TryType, Type.Parse);

            set => this.SetOptional(TryType, value);
        }

        public Expression Body
        {
            get => this.GetRequired(TryBody, Expression.Parse);

            set => this.SetRequired(TryBody, value);
        }

        public Expression? Finally
        {
            get => this.GetOptional(TryFinally, Expression.Parse);

            set => this.SetOptional(TryFinally, value);
        }

        public Expression? Fault
        {
            get => this.GetOptional(TryFault, Expression.Parse);

            set => this.SetOptional(TryFault, value);
        }

        public ICollection<Catch> Handlers => this.Collection(TryHandlers, Catch.Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.MakeTry(
            this.Type?.SystemType,
            this.Body.LinqExpression,
            this.Finally?.LinqExpression,
            this.Fault?.LinqExpression,
            from h in this.Handlers select h.LinqCatchBlock);
    }
}
