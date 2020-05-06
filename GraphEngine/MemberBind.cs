// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class MemberBind : BaseBind
    {
        [DebuggerStepThrough]
        internal MemberBind(INode node)
            : base(node)
        {
        }

        public IEnumerable<BaseBind> Bindings => List<BaseBind>(MemberBindBindings);

        public override Linq.MemberBinding LinqMemberBinding => Linq.Expression.MemberBind(this.Member.ReflectionMember, this.Bindings.Select(binding => binding.LinqMemberBinding));
    }
}
