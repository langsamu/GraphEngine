// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class MemberInit : Expression
    {
        [DebuggerStepThrough]
        internal MemberInit(INode node)
            : base(node)
        {
        }

        public New NewExpression
        {
            get => this.GetRequired(MemberInitNewExpression, AsNew);

            set => this.SetRequired(MemberInitNewExpression, value);
        }

        public ICollection<BaseBind> Bindings => this.Collection(MemberInitBindings, AsBaseBind);

        public override Linq.Expression LinqExpression => Linq.Expression.MemberInit(this.NewExpression.LinqNewExpression, this.Bindings.Select(binding => binding.LinqMemberBinding));
    }
}
