// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using VDS.RDF;

    public class VariableExpressionNode : ParameterExpressionNode
    {
        public VariableExpressionNode(INode node)
            : base(node)
        {
        }
    }
}