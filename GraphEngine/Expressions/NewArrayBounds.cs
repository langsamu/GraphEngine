// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class NewArrayBounds : Expression
    {
        [DebuggerStepThrough]
        internal NewArrayBounds(INode node)
            : base(node)
        {
        }

        public Type Type
        {
            get => this.GetRequired<Type>(NewArrayBoundsType);

            set => this.SetRequired(NewArrayBoundsType, value);
        }

        public ICollection<Expression> Bounds => this.Collection<Expression>(NewArrayBoundsBounds);

        public override Linq.Expression LinqExpression => Linq.Expression.NewArrayBounds(this.Type.SystemType, this.Bounds.LinqExpressions());
    }
}
