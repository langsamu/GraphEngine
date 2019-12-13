// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    // TODO: Add name
    public class Parameter : Expression
    {
        private static readonly IDictionary<INode, Linq.ParameterExpression> Cache = new Dictionary<INode, Linq.ParameterExpression>();

        [DebuggerStepThrough]
        public Parameter(INode node)
            : base(node)
        {
        }

        public Type Type => Vocabulary.ParameterType.ObjectsOf(this).Select(Type.Parse).Single();

        public override Linq.Expression LinqExpression => this.LinqParameter;

        public Linq.ParameterExpression LinqParameter
        {
            get
            {
                if (!Cache.TryGetValue(this, out var param))
                {
                    param = Cache[this] = Linq.Expression.Parameter(this.Type.SystemType);
                }

                return param;
            }
        }

        internal static new Parameter Parse(INode node) => new Parameter(node);
    }
}