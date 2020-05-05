﻿// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    // TODO: Implement
    public class Catch : Expression
    {
        [DebuggerStepThrough]
        internal Catch(INode node)
            : base(node)
        {
            throw new NotImplementedException();
        }

        public override Linq.Expression LinqExpression => default;
    }
}
