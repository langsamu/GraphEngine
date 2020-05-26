// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
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
            get => this.GetRequired(ElementInitAddMethod, Method.Parse);

            set => this.SetRequired(ElementInitAddMethod, value);
        }

        public ICollection<Expression> Arguments => this.Collection(ElementInitArguments, Expression.Parse);

        public Linq.ElementInit LinqElementInit => Linq.Expression.ElementInit(this.AddMethod.ReflectionMethod, this.Arguments.LinqExpressions());

        internal static ElementInit Parse(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return new ElementInit(node);
        }
    }
}
