// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;

    [TestClass]
    public class SetterTests
    {
        [TestMethod]
        public void All()
        {
            using var g = new GraphEngine.Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    :arrayIndexArray [
        :parameterType [
            :typeName ""System.Int32[]"" ;
        ] ;
    ] ;
    :arrayIndexIndexes (
        [
            :parameterType [
                :typeName ""System.Int32"" ;
            ] ;
        ]
        [
            :parameterType [
                :typeName ""System.Int64"" ;
            ] ;
        ]
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var aa = new Collection<Expression>(s, Vocabulary.ArrayIndexIndexes, Expression.Parse);
            foreach (Parameter index in aa)
            {
                Console.WriteLine(index.Type.Name);
            }

            var item = new Parameter(s.Graph.CreateBlankNode());
            var type = new GraphEngine.Type(s.Graph.CreateBlankNode());
            type.Name = "X";
            var typeA = new GraphEngine.Type(s.Graph.CreateBlankNode());
            typeA.Name = "Y";
            type.Arguments.Add(typeA);
            item.Type = type;

            aa.Add(item);

            foreach (Parameter index in aa)
            {
                Console.WriteLine(index.Type.Name);
            }
        }
    }
}
