// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class ElementInit : Node
    {
        [DebuggerStepThrough]
        internal ElementInit(INode node)
            : base(node)
        {
        }

        public Method AddMethod => Required<Method>(ElementInitAddMethod);

        public IEnumerable<Expression> Arguments => List<Expression>(ElementInitArguments);

        public Linq.ElementInit LinqElementInit => Linq.Expression.ElementInit(this.AddMethod.ReflectionMethod, this.Arguments.LinqExpressions());
    }
}
