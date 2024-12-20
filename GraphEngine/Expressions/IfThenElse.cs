// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class IfThenElse(NodeWithGraph node) : Condition(node, Vocabulary.IfThenElse)
{
    public override Linq.Expression LinqExpression => Linq.Expression.IfThenElse(Test.LinqExpression, IfTrue.LinqExpression, IfFalse.LinqExpression);
}
