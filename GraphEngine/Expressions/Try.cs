// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Try : Expression
    {
        [DebuggerStepThrough]
        internal Try(INode node)
            : base(node)
        {
        }

        public Type? Type
        {
            get => this.GetOptional(TryType, AsType);

            set => this.SetOptional(TryType, value);
        }

        public Expression Body
        {
            get => this.GetRequired(TryBody, AsExpression);

            set => this.SetRequired(TryBody, value);
        }

        public Expression? Finally
        {
            get => this.GetOptional(TryFinally, AsExpression);

            set => this.SetOptional(TryFinally, value);
        }

        public Expression? Fault
        {
            get => this.GetOptional(TryFault, AsExpression);

            set => this.SetOptional(TryFault, value);
        }

        public ICollection<CatchBlock> Handlers => this.Collection(TryHandlers, AsCatchBlock);

        public override Linq.Expression LinqExpression => Linq.Expression.MakeTry(this.Type?.SystemType, this.Body.LinqExpression, this.Finally?.LinqExpression, this.Fault?.LinqExpression, this.Handlers.Select(h => h.LinqCatchBlock));
    }
}
