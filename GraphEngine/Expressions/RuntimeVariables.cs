// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class RuntimeVariables : Expression
    {
        [DebuggerStepThrough]
        internal RuntimeVariables(INode node)
            : base(node)
        {
        }

        public ICollection<Parameter> Variables => this.Collection(RuntimeVariablesVariables, Parameter.Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.RuntimeVariables(this.Variables.Select(e => e.LinqParameter));
    }
}
