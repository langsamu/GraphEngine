namespace GraphEngine
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;

    [TestClass]
    public class CallTests
    {
        [TestMethod]
        public void Static_no_types_no_arguments()
        {
            var expected = Expression.Call(typeof(C1), nameof(C1.M1), Array.Empty<Type>(), Array.Empty<Expression>());

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :type ""GraphEngine.C1"" ;
    :method ""M1"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Assert.AreEqual(GetDebugView(expected), GetDebugView(actual));
        }

        [TestMethod]
        public void Static_types_no_arguments()
        {
            var expected = Expression.Call(typeof(C1), nameof(C1.M2), new[] { typeof(object) }, Array.Empty<Expression>());

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :type ""GraphEngine.C1"" ;
    :method ""M2"" ;
    :typeArguments (
        ""System.Object""
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Assert.AreEqual(GetDebugView(expected), GetDebugView(actual));
        }

        [TestMethod]
        public void Static_no_types_arguments()
        {
            var expected = Expression.Call(typeof(C1), nameof(C1.M3), Array.Empty<Type>(), new[] { Expression.Constant(0) });

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :type ""GraphEngine.C1"" ;
    :method ""M3"" ;
    :arguments (
        0
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Assert.AreEqual(GetDebugView(expected), GetDebugView(actual));
        }

        [TestMethod]
        public void Static_types_arguments()
        {
            var expected = Expression.Call(typeof(C1), nameof(C1.M4), new[] { typeof(object) }, new[] { Expression.Constant(null) });

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :type ""GraphEngine.C1"" ;
    :method ""M4"" ;
    :typeArguments (
        ""System.Object""
    ) ;
    :arguments (
        0
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Assert.AreEqual(GetDebugView(expected), GetDebugView(actual));
        }

        [TestMethod]
        public void Instance_no_types_no_arguments()
        {
            var expected = Expression.Call(Expression.New(typeof(C1)), nameof(C1.M5), Array.Empty<Type>(), Array.Empty<Expression>());

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :instance [
        a :New ;
        :type ""GraphEngine.C1""
    ] ;
    :method ""M5"" ;
.
");
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Assert.AreEqual(GetDebugView(expected), GetDebugView(actual));
        }

        [TestMethod]
        public void Instance_types_no_arguments()
        {
            var expected = Expression.Call(Expression.New(typeof(C1)), nameof(C1.M6), new[] { typeof(object) }, Array.Empty<Expression>());

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :instance [
        a :New ;
        :type ""GraphEngine.C1""
    ] ;
    :method ""M6"" ;
    :typeArguments (
        ""System.Object""
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Assert.AreEqual(GetDebugView(expected), GetDebugView(actual));
        }

        [TestMethod]
        public void Instance_no_types_arguments()
        {
            var expected = Expression.Call(Expression.New(typeof(C1)), nameof(C1.M7), Array.Empty<Type>(), new[] { Expression.Constant(null) });

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :type ""GraphEngine.C1"" ;
    :method ""M7"" ;
    :arguments (
        0
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Assert.AreEqual(GetDebugView(expected), GetDebugView(actual));
        }

        [TestMethod]
        public void Instance_types_arguments()
        {
            var expected = Expression.Call(Expression.New(typeof(C1)), nameof(C1.M8), new[] { typeof(object) }, new[] { Expression.Constant(null) });

            using var g = new Graph();
            g.LoadFromString(@"
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix : <http://example.com/> .

:s
    a :Call ;
    :type ""GraphEngine.C1"" ;
    :method ""M8"" ;
    :typeArguments (
        ""System.Object""
    ) ;
    :arguments (
        0
    ) ;
.
");
            var s = g.GetUriNode(":s");

            var actual = ExpressionNode.Parse(s).Expression;

            Assert.AreEqual(GetDebugView(expected), GetDebugView(actual));
        }

        private static string GetDebugView(Expression exp) => typeof(Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(exp) as string;
    }

    public class C1
    {
        public static void M1() { }

        public static void M2<T>() { }

        public static void M3(object arg) { }

        public static void M4<T>(object arg) { }

        public void M5() { }

        public void M6<T>() { }

        public void M7(object arg) { }

        public void M8<T>(object arg) { }
    }
}
