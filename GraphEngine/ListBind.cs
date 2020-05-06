// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class ListBind : BaseBind
    {
        [DebuggerStepThrough]
        internal ListBind(INode node)
            : base(node)
        {
        }

        public IEnumerable<ElementInit> Initializers => List<ElementInit>(ListBindInitializers);

        public override Linq.MemberBinding LinqMemberBinding => Linq.Expression.ListBind(this.Member.ReflectionMember, this.Initializers.Select(initializer => initializer.LinqElementInit));
    }
}
