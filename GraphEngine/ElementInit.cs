// MIT License, Copyright 2020 Samu Lang

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

        public Method AddMethod
        {
            get => this.GetRequired(ElementInitAddMethod, AsMethod);

            set => this.SetRequired(ElementInitAddMethod, value);
        }

        public ICollection<Expression> Arguments => this.Collection(ElementInitArguments, AsExpression);

        public Linq.ElementInit LinqElementInit => Linq.Expression.ElementInit(this.AddMethod.ReflectionMethod, this.Arguments.LinqExpressions());
    }
}
