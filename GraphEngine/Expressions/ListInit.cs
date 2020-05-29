// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class ListInit : Expression
    {
        [DebuggerStepThrough]
        internal ListInit(INode node)
            : base(node)
        {
        }

        public New NewExpression
        {
            get => this.GetRequired(ListInitNewExpression, New.Parse);

            set => this.SetRequired(ListInitNewExpression, value);
        }

        public ICollection<ElementInit> Initializers => this.Collection(ListInitInitializers, ElementInit.Parse);

        public override Linq.Expression LinqExpression => Linq.Expression.ListInit(this.NewExpression.LinqNewExpression, this.Initializers.Select(i => i.LinqElementInit));
    }
}
