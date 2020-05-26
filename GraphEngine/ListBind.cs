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
            this.RdfType = Vocabulary.ListBind;
        }

        public ICollection<ElementInit> Initializers => this.Collection(ListBindInitializers, ElementInit.Parse);

        public override Linq.MemberBinding LinqMemberBinding => Linq.Expression.ListBind(this.Member.ReflectionMember, this.Initializers.Select(initializer => initializer.LinqElementInit));
    }
}
