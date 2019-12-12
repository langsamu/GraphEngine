// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine.Tests
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;

    [TestClass]
    public class SchemaTests
    {
        [TestMethod]
        public void POC()
        {
            using var g = new Graph();
            g.LoadFromEmbeddedResource("GraphEngine.Tests.Resources.schema.ttl,GraphEngine.Tests");
        }
    }
}
