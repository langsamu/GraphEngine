// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class Field : MemberAccess
    {
        [DebuggerStepThrough]
        internal Field(INode node)
            : base(node)
        {
            this.RdfType = Vocabulary.Field;
        }

        public override Linq.Expression LinqExpression
        {
            get
            {
                if (this.Type is Type type)
                {
                    return Linq.Expression.Field(this.Expression?.LinqExpression, type.SystemType, this.Name);
                }

                return Linq.Expression.Field(this.Expression?.LinqExpression, this.Name);
            }
        }
    }
}
