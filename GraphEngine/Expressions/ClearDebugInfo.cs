// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using Linq = System.Linq.Expressions;

    public class ClearDebugInfo : DebugInfo
    {
        [DebuggerStepThrough]
        internal ClearDebugInfo(NodeWithGraph node)
            : base(node)
        {
            this.RdfType = Vocabulary.ClearDebugInfo;
        }

        public override Linq.Expression LinqExpression => Linq.Expression.ClearDebugInfo(this.Document.LinqDocument);
    }
}
