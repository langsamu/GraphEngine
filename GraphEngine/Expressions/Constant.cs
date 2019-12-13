// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using VDS.RDF;
    using VDS.RDF.Parsing;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Constant : Expression
    {
        [DebuggerStepThrough]
        internal Constant(INode node)
            : base(node)
        {
        }

        public Type Type => Optional<Type>(ConstantType);

        // TODO: Handle datatypes unknown to RDF e.g. "abc"^^http://example.com/System.Object
        public override Linq.Expression LinqExpression
        {
            get
            {
                if (this.Type is Type type)
                {
                    return Linq.Expression.Constant(this.Value, type.SystemType);
                }

                return Linq.Expression.Constant(this.Value);
            }
        }

        private object Value
        {
            get
            {
                var valueNode = ConstantValue.ObjectOf(this);

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
                                return long.Parse(literalNode.Value, CultureInfo.InvariantCulture);

                            case XmlSpecsHelper.XmlSchemaDataTypeInt:
                                return int.Parse(literalNode.Value, CultureInfo.InvariantCulture);

                            default:
                                throw new Exception($"unknown datatype {literalNode.DataType.AbsoluteUri} on node {literalNode}");
                        }

                    default:
                        throw new Exception($"unknown node type {valueNode.NodeType} on node {valueNode}");
                }
            }
        }
    }
}
