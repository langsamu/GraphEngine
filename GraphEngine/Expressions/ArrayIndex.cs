﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class ArrayIndex : Expression
    {
        [DebuggerStepThrough]
        internal ArrayIndex(INode node)
            : base(node)
        {
        }

        public Expression Array
        {
            get => this.GetRequired(ArrayIndexArray, Expression.Parse);

            set => this.SetRequired(ArrayIndexArray, value);
        }

        public Expression? Index
        {
            get => this.GetOptional(ArrayIndexIndex, Expression.Parse);

            set => this.SetOptional(ArrayIndexIndex, value);
        }

        public ICollection<Expression> Indexes => this.Collection(ArrayIndexIndexes, Expression.Parse);

        public override Linq.Expression LinqExpression
        {
            get
            {
                if (this.Index is Expression index)
                {
                    return Linq.Expression.ArrayIndex(this.Array.LinqExpression, index.LinqExpression);
                }

                return Linq.Expression.ArrayIndex(this.Array.LinqExpression, this.Indexes.LinqExpressions());
            }
        }
    }
}
