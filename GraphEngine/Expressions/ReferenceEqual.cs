// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class ReferenceEqual : Binary
{
    [DebuggerStepThrough]
    internal ReferenceEqual(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.ReferenceEqual;

    public override Linq.Expression LinqExpression => Linq.Expression.ReferenceEqual(this.Left.LinqExpression, this.Right.LinqExpression);
}
