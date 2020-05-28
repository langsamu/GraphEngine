// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class NewArrayBounds : NewArray
    {
        [DebuggerStepThrough]
        internal NewArrayBounds(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.NewArrayBounds;
        }

        protected override NewArrayExpressionFactory FactoryMethod => Linq.Expression.NewArrayBounds;
    }
}
