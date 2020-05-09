// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Bind : BaseBind
    {
        [DebuggerStepThrough]
        internal Bind(INode node)
            : base(node)
        {
        }

        public Expression Expression
        {
            get => this.GetRequired<Expression>(BindExpression);

            set => this.SetRequired(BindExpression, value);
        }

        public override MemberBinding LinqMemberBinding => Linq.Expression.Bind(this.Member.ReflectionMember, this.Expression.LinqExpression);
    }
}