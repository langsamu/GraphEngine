﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    // TODO: Implement
    public class ListInit : Expression
    {
        [DebuggerStepThrough]
        internal ListInit(INode node)
            : base(node)
        {
            throw new NotImplementedException();
        }

        public override Linq.Expression LinqExpression => throw new InvalidOperationException();
    }
}
