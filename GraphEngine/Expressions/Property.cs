// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Property(NodeWithGraph node) : MemberAccess(node, Vocabulary.Property)
{
    public ICollection<Expression> Arguments => this.Collection(PropertyArguments, Expression.Parse);

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
