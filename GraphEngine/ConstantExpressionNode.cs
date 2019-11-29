﻿namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using System.Linq;
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
                var valueNode = Vocabulary.Value.ObjectOf(this);

                switch (valueNode)
                {
                    case IUriNode uriNode when uriNode.NodeType == NodeType.Uri:
                        return uriNode.Uri;

                    case ILiteralNode literalNode when literalNode.NodeType == NodeType.Literal:
                        if (literalNode.DataType is null)
                        {
                            return literalNode.Value;
                        }

                        switch (literalNode.DataType.AbsoluteUri)
                        {
                            case XmlSpecsHelper.XmlSchemaDataTypeInteger:
                                return long.Parse(literalNode.Value);

                            case XmlSpecsHelper.XmlSchemaDataTypeInt:
                                return int.Parse(literalNode.Value);

                            default:
                                throw new Exception($"unknown datatype {literalNode.DataType.AbsoluteUri} on node {literalNode}");
                        }

                    default:
                        throw new Exception($"unknown node type {valueNode.NodeType} on node {valueNode}");
                }
            }
        }

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        // TODO: Handle datatypes unknown to RDF e.g. "abc"^^http://example.com/System.Object
        public override Expression Expression
        {
            get
            {
                if (Type is TypeNode type)
                {
                    return Expression.Constant(Value, type.Type);
                }

                return Expression.Constant(Value);
            }
        }
    }
}
