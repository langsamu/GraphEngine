// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq=System.Linq.Expressions;

    public class Try : Expression
    {
        [DebuggerStepThrough]
        internal Try(INode node)
            : base(node)
        {
        }

        public Type Type => Vocabulary.TryType.ObjectsOf(this).Select(Type.Parse).SingleOrDefault();

        public Expression Body => Vocabulary.TryBody.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public Expression Finally => Vocabulary.TryFinally.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public Expression Fault => Vocabulary.TryFault.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public IEnumerable<CatchBlock> Handlers => Vocabulary.TryHandlers.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(CatchBlock.Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.MakeTry(this.Type?.SystemType, this.Body?.LinqExpression, this.Finally?.LinqExpression, this.Fault?.LinqExpression, this.Handlers.Select(h => h.LinqCatchBlock));
    }
}
