// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class IfThen(NodeWithGraph node) : Condition(node, Vocabulary.IfThen)
{
    public override Linq.Expression LinqExpression => Linq.Expression.IfThen(this.Test.LinqExpression, this.IfTrue.LinqExpression);
}
