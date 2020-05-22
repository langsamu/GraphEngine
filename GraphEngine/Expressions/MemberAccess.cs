// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;

    public abstract class MemberAccess : Expression
    {
        [DebuggerStepThrough]
        internal MemberAccess(INode node)
            : base(node)
        {
        }

        public Expression? Expression
        {
            get => this.GetOptional<Expression>(MemberAccessExpression);

            set => this.SetOptional(MemberAccessExpression, value);
        }

        public string Name
        {
            get => this.GetRequired<string>(MemberAccessName);

            set => this.SetRequired(MemberAccessName, value);
        }

        public Type? Type
        {
            get => this.GetOptional<Type>(MemberAccessType);

            set => this.SetOptional(MemberAccessType, value);
        }
    }
}
