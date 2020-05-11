﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class IfThenElse : Condition
    {
        [DebuggerStepThrough]
        internal IfThenElse(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.IfThenElse;
        }

        public override Linq.Expression LinqExpression => Linq.Expression.IfThenElse(this.Test.LinqExpression, this.IfTrue.LinqExpression, this.IfFalse.LinqExpression);
    }
}
