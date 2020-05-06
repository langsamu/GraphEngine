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

        public Type? Type => Optional<Type>(TryType);

        public Expression? Body => Optional<Expression>(TryBody);

        public Expression? Finally => Optional<Expression>(TryFinally);

        public Expression? Fault => Optional<Expression>(TryFault);

        public IEnumerable<CatchBlock> Handlers => List<CatchBlock>(TryHandlers);

        public override Linq.Expression LinqExpression => Linq.Expression.MakeTry(this.Type?.SystemType, this.Body?.LinqExpression, this.Finally?.LinqExpression, this.Fault?.LinqExpression, this.Handlers.Select(h => h.LinqCatchBlock));
    }
}
