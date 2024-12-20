// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class RuntimeVariables(NodeWithGraph node) : Expression(node)
{
    public ICollection<Parameter> Variables => Collection(RuntimeVariablesVariables, Parameter.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.RuntimeVariables(from e in Variables select e.LinqParameter);
}
