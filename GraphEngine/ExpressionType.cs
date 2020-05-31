// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class ExpressionType : Node
    {
        [DebuggerStepThrough]
        public ExpressionType(INode node)
            : base(node)
        {
        }

        public Linq.ExpressionType LinqExpressionType =>
            this switch
            {
                var n when n.Equals(Vocabulary.ExpressionTypes.Add) => Linq.ExpressionType.Add,

                var unknown => throw new InvalidOperationException($"Unknown expression type node {unknown}"),
            };

        internal static ExpressionType Create(Linq.ExpressionType expressionType) =>
            Parse(
                expressionType switch
                {
                    Linq.ExpressionType.Add => Vocabulary.ExpressionTypes.Add,

                    var unknown => throw new InvalidOperationException($"Uknown expression type {unknown}")
                });

        internal static ExpressionType Parse(INode node) =>
            new ExpressionType(node);
    }
}
