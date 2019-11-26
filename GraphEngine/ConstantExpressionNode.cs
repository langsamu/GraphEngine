namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using VDS.RDF;
    using VDS.RDF.Parsing;

    public class ConstantExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal ConstantExpressionNode(INode node) : base(node) { }

        private object Value
        {
            get
            {
                var literal = (ILiteralNode)this;

                if (literal.DataType is null)
                {
                    return literal.Value;
                }

                switch (literal.DataType.AbsoluteUri)
                {
                    case XmlSpecsHelper.XmlSchemaDataTypeInteger:
                        return long.Parse(literal.Value);

                    case XmlSpecsHelper.XmlSchemaDataTypeInt:
                        return int.Parse(literal.Value);

                    default:
                        throw new Exception($"unknown literal datatype {literal.DataType.AbsoluteUri}");
                }
            }
        }

        public override Expression Expression => Expression.Constant(this.Value);
    }
}
