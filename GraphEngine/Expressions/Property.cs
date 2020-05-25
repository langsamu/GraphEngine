// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Property : MemberAccess
    {
        [DebuggerStepThrough]
        internal Property(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.Property;
        }

        public ICollection<Expression> Arguments => this.Collection(PropertyArguments, AsExpression);

        public override Linq.Expression LinqExpression
        {
            get
            {
                var arguments = this.Arguments;
                if (arguments.Any())
                {
                    return Linq.Expression.Property(this.Expression?.LinqExpression, this.Name, arguments.LinqExpressions().ToArray());
                }

                if (this.Type is Type type)
                {
                    return Linq.Expression.Property(this.Expression?.LinqExpression, type.SystemType, this.Name);
                }

                return Linq.Expression.Property(this.Expression?.LinqExpression, this.Name);
            }
        }
    }
}
