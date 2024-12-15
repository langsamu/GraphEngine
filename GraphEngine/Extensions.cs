// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Nodes;
using VDS.RDF.Parsing;
using Linq = System.Linq.Expressions;

public static class Extensions
{
    public static NodeWithGraph In(this INode node, IGraph graph) => new (node, graph);

    internal static IEnumerable<NodeWithGraph> ObjectsOf(this INode predicate, NodeWithGraph subject) =>
        from t in subject.Graph.GetTriplesWithSubjectPredicate(subject, predicate)
        select t.Object.In(subject.Graph);

    internal static NodeWithGraph? ObjectOf(this INode predicate, NodeWithGraph subject) => predicate.ObjectsOf(subject).FirstOrDefault();

    internal static IEnumerable<NodeWithGraph> InstancesOf(this IGraph graph, INode @class) =>
        from t in graph.GetTriplesWithPredicateObject(Vocabulary.RdfType, @class)
        select t.Subject.In(graph);

    internal static INode AsNode(this object value, IGraph graph) =>
        value switch
        {
            INode node => node,
            Uri uri => graph.CreateUriNode(uri),
            long number => graph.CreateLiteralNode(number.ToString(CultureInfo.InvariantCulture), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeInteger)),
            int number => graph.CreateLiteralNode(number.ToString(CultureInfo.InvariantCulture), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeInt)),
            Guid guid => graph.CreateUriNode(new Uri($"urn:uuid:{guid}")),
            bool bit => new BooleanNode(bit),
            _ => graph.CreateLiteralNode(value.ToString())
        };

    internal static IEnumerable<Linq.Expression> LinqExpressions(this IEnumerable<Expression> expressions) =>
        from e in expressions
        select e.LinqExpression;

    internal static object AsObject(this NodeWithGraph wrapper) => wrapper switch
    {
        IUriNode { NodeType: NodeType.Uri } uriNode => uriNode.Uri,
        ILiteralNode { NodeType: NodeType.Literal, DataType.AbsoluteUri: XmlSpecsHelper.XmlSchemaDataTypeString } literalNode => literalNode.Value,
        ILiteralNode { NodeType: NodeType.Literal, DataType.AbsoluteUri: XmlSpecsHelper.XmlSchemaDataTypeInteger } literalNode => long.Parse(literalNode.Value, CultureInfo.InvariantCulture),
        ILiteralNode { NodeType: NodeType.Literal, DataType.AbsoluteUri: XmlSpecsHelper.XmlSchemaDataTypeInt } literalNode => int.Parse(literalNode.Value, CultureInfo.InvariantCulture),
        _ => wrapper.Original,
    };

    // See https://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Ast/BinaryExpression.cs,374
    internal static bool IsReferenceComparison(this Linq.BinaryExpression expression)
    {
        var left = expression.Left.Type;
        var right = expression.Right.Type;
        var method = expression.Method;
        var kind = expression.NodeType;

        return (kind == Linq.ExpressionType.Equal || kind == Linq.ExpressionType.NotEqual) &&
            method == null && !left.IsValueType && !right.IsValueType;
    }

    // See https://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Ast/TypeUtils.cs,d20b8274c8dc7b89
    internal static bool AreEquivalent(System.Type t1, System.Type t2) => t1 == t2 || t1.IsEquivalentTo(t2);
}
