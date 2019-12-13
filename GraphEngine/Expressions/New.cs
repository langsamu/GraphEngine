// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class New : Expression
    {
        [DebuggerStepThrough]
        internal New(INode node)
            : base(node)
        {
        }

        public IEnumerable<Expression> Arguments => Vocabulary.NewArguments.ObjectsOf(this).SelectMany(this.Graph.GetListItems).Select(Parse);

        public Type Type => Vocabulary.NewType.ObjectsOf(this).Select(Type.Parse).Single();

        public override Linq.Expression LinqExpression
        {
            get
            {
                var argumentExpressions = this.Arguments.Select(arg => arg.LinqExpression);
                var types = argumentExpressions.Select(expression => expression.Type).ToArray();
                var constructor = this.Type.SystemType.GetConstructor(types);

                return Linq.Expression.New(constructor, argumentExpressions);
            }
        }
    }
}
