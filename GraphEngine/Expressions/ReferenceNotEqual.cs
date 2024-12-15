// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Diagnostics;
using Linq = System.Linq.Expressions;

public class ReferenceNotEqual : Binary
{
    [DebuggerStepThrough]
    internal ReferenceNotEqual(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.ReferenceNotEqual;

    public override Linq.Expression LinqExpression => Linq.Expression.ReferenceNotEqual(this.Left.LinqExpression, this.Right.LinqExpression);
}
