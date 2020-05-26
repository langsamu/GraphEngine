// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Bind : BaseBind
    {
        [DebuggerStepThrough]
        internal Bind(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.Bind;
        }

        public Expression Expression
        {
            get => this.GetRequired(BindExpression, Expression.Parse);

            set => this.SetRequired(BindExpression, value);
        }

        public override Linq.MemberBinding LinqMemberBinding => Linq.Expression.Bind(this.Member.ReflectionMember, this.Expression.LinqExpression);
    }
}