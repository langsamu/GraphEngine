// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using VDS.RDF;
    using VDS.RDF.Parsing;
    using Linq = System.Linq.Expressions;

    internal static class Extensions
    {
        internal static IEnumerable<INode> ObjectsOf(this INode predicate, INode subject) =>
            from t in subject.Graph.GetTriplesWithSubjectPredicate(subject, predicate)
            select t.Object;

        internal static INode? ObjectOf(this INode predicate, INode subject) =>
            predicate.ObjectsOf(subject).FirstOrDefault();

        internal static IEnumerable<INode> InstancesOf(this IGraph graph, INode @class) =>
            from t in graph.GetTriplesWithPredicateObject(Vocabulary.RdfType, @class)
            select t.Subject;

        internal static INode AsNode(this object value, IGraph graph) =>
            value switch
            {
                INode node => node,
                Uri uri => graph.CreateUriNode(uri),
                long number => graph.CreateLiteralNode(number.ToString(CultureInfo.InvariantCulture), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeInteger)),
                int number => graph.CreateLiteralNode(number.ToString(CultureInfo.InvariantCulture), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeInt)),
                Guid guid => graph.CreateUriNode(new Uri("urn:uuid:" + guid.ToString())),
                _ => graph.CreateLiteralNode(value.ToString())
            };

        internal static IEnumerable<Linq.Expression> LinqExpressions(this IEnumerable<Expression> expressions) =>
            expressions.Select(expression => expression.LinqExpression);

        internal static object AsObject(this INode node)
        {
            return node switch
            {
                IUriNode { NodeType: NodeType.Uri } uriNode => uriNode.Uri,
                ILiteralNode { NodeType: NodeType.Literal, DataType: null, Language: "" } literalNode => literalNode.Value,
                ILiteralNode { NodeType: NodeType.Literal, DataType: { AbsoluteUri: XmlSpecsHelper.XmlSchemaDataTypeInteger } } literalNode => long.Parse(literalNode.Value, CultureInfo.InvariantCulture),
                ILiteralNode { NodeType: NodeType.Literal, DataType: { AbsoluteUri: XmlSpecsHelper.XmlSchemaDataTypeInt } } literalNode => int.Parse(literalNode.Value, CultureInfo.InvariantCulture),
                _ => node,
            };
        }

        // TODO: Remove
        internal static string GetDebugView(this Linq.Expression exp) => (string)typeof(Linq.Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(exp);
    }
}
