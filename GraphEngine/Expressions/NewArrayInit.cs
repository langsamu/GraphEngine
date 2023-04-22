// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using Linq = System.Linq.Expressions;

    public class NewArrayInit : NewArray
    {
        [DebuggerStepThrough]
        internal NewArrayInit(NodeWithGraph node)
            : base(node)
        {
            this.RdfType = Vocabulary.NewArrayInit;
        }

        protected override NewArrayExpressionFactory FactoryMethod => Linq.Expression.NewArrayInit;
    }
}
