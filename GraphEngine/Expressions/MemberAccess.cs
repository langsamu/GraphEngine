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
            get => this.GetOptional(MemberAccessExpression, AsExpression);

            set => this.SetOptional(MemberAccessExpression, value);
        }

        public string Name
        {
            get => this.GetRequired(MemberAccessName, AsString);

            set => this.SetRequired(MemberAccessName, value);
        }

        public Type? Type
        {
            get => this.GetOptional(MemberAccessType, AsType);

            set => this.SetOptional(MemberAccessType, value);
        }
    }
}
