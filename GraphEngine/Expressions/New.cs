// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

// TODO: Add overloads
// TODO: Create ConstructorInfo node
public class New(NodeWithGraph node) : Expression(node)
{
    public ICollection<Expression> Arguments => Collection(NewArguments, Expression.Parse);

    public Type Type
    {
        get => GetRequired(NewType, Type.Parse);

        set => SetRequired(NewType, value);
    }

    public override Linq.Expression LinqExpression => LinqNewExpression;

    internal Linq.NewExpression LinqNewExpression
    {
        get
        {
            var argumentExpressions = Arguments.LinqExpressions();
            var types = (from expression in argumentExpressions select expression.Type).ToArray();
            var constructor = Type.SystemType.GetConstructor(types);

            return Linq.Expression.New(constructor, argumentExpressions);
        }
    }

    internal static new New Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new New(node)
    };
}
