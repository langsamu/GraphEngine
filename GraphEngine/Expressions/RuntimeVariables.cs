// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class RuntimeVariables : Expression
{
    [DebuggerStepThrough]
    internal RuntimeVariables(NodeWithGraph node)
        : base(node)
    {
    }

    public ICollection<Parameter> Variables => this.Collection(RuntimeVariablesVariables, Parameter.Parse);

    public override Linq.Expression LinqExpression => Linq.Expression.RuntimeVariables(from e in this.Variables select e.LinqParameter);
}
