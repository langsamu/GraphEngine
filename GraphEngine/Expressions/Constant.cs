// MIT License, Copyright 2020 Samu Lang

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

        public object Value
        {
            get
            {
                var valueNode = ConstantValue.ObjectOf(this) ?? throw new InvalidOperationException("missing value");

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
                                throw new InvalidOperationException($"unknown datatype {literalNode.DataType.AbsoluteUri} on node {literalNode}");
                        }

                    default:
                        throw new InvalidOperationException($"unknown node type {valueNode.NodeType} on node {valueNode}");
                }
            }

            set => this.SetRequired(ConstantValue, value);
        }

        public Type? Type
        {
            get => this.GetOptional<Type>(ConstantType);

            set => this.SetOptional(ConstantType, value);
        }

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
    }
}
