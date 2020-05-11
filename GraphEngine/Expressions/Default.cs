// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Default : Expression
    {
        [DebuggerStepThrough]
        internal Default(INode node)
            : base(node)
        {
        }

        public Type Type
        {
            get => this.GetRequired<Type>(DefaultType);

            set => this.SetRequired(DefaultType, value);
        }

        public override Linq.Expression LinqExpression => Linq.Expression.Default(this.Type.SystemType);
    }
}
