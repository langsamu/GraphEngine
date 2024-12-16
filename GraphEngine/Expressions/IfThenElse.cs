// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class IfThenElse : Condition
{
    [DebuggerStepThrough]
    internal IfThenElse(NodeWithGraph node)
        : base(node)
        => this.RdfType = Vocabulary.IfThenElse;

    public override Linq.Expression LinqExpression => Linq.Expression.IfThenElse(this.Test.LinqExpression, this.IfTrue.LinqExpression, this.IfFalse.LinqExpression);
}
