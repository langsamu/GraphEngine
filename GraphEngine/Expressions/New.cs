// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class New : Expression
    {
        [DebuggerStepThrough]
        internal New(INode node)
            : base(node)
        {
        }

        public ICollection<Expression> Arguments => this.Collection<Expression>(NewArguments);

        public Type Type
        {
            get => this.GetRequired<Type>(NewType);

            set => this.SetRequired(NewType, value);
        }

        public override Linq.Expression LinqExpression => this.LinqNewExpression;

        internal Linq.NewExpression LinqNewExpression
        {
            get
            {
                var argumentExpressions = this.Arguments.LinqExpressions();
                var types = argumentExpressions.Select(expression => expression.Type).ToArray();
                var constructor = this.Type.SystemType.GetConstructor(types);

                return Linq.Expression.New(constructor, argumentExpressions);
            }
        }
    }
}
