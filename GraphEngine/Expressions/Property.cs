// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class Property(NodeWithGraph node) : MemberAccess(node, Vocabulary.Property)
{
    public ICollection<Expression> Arguments => Collection(PropertyArguments, Expression.Parse);

    public override Linq.Expression LinqExpression
    {
        get
        {
            var arguments = Arguments;
            if (arguments.Any())
            {
                return Linq.Expression.Property(Expression?.LinqExpression, Name, arguments.LinqExpressions().ToArray());
            }

            if (Type is Type type)
            {
                return Linq.Expression.Property(Expression?.LinqExpression, type.SystemType, Name);
            }

            return Linq.Expression.Property(Expression?.LinqExpression, Name);
        }
    }
}
