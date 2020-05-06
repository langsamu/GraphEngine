// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;

    [TestClass]
    public class SchemaTests
    {
        [TestMethod]
        public void Valid()
        {
            using var g = new Graph();
            g.LoadFromEmbeddedResource("GraphEngine.Tests.Resources.schema.ttl,GraphEngine.Tests");
        }
    }
}
