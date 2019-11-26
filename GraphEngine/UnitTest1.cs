namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VDS.RDF;
    using VDS.RDF.Nodes;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using var g = new Graph();
            g.LoadFromString(@"
@prefix : <http://example.com/> .

:s
    a :Add ;
    :left [
        a :Add ;
        :left 1 ;
        :right 2 ;
    ] ;
    :right 3 ;
.
");

            var s = g.GetUriNode(":s");

            var result = ExpressionNode.Parse(s).Expression;

            Console.WriteLine(result.ToString());
        }
    }

    public abstract class ExpressionNode : WrapperNode
    {
        protected ExpressionNode(INode node) : base(node) { }

        public abstract Expression Expression { get; }

        public static ExpressionNode Parse(INode node)
        {
            switch (node.NodeType)
            {
                case NodeType.Blank:
                case NodeType.Uri:
                    return new AddExpressionNode(node);

                case NodeType.Literal:
                    return new ConstantExpressionNode(node);

                default:
                    throw new Exception("unknown node type");
            }
        }
    }

    public class ConstantExpressionNode : ExpressionNode
    {
        internal ConstantExpressionNode(INode node) : base(node) { }

        private object Value
        {
            get
            {
                return this.AsValuedNode().AsInteger();
            }
        }

        public override Expression Expression => Expression.Constant(this.Value);
    }

    public abstract class BinaryExpressionNode : ExpressionNode
    {
        protected BinaryExpressionNode(INode node) : base(node) { }

        public ExpressionNode Left => ExpressionNode.Parse(Vocabulary.Left.ObjectOf(this));

        public ExpressionNode Right => ExpressionNode.Parse(Vocabulary.Right.ObjectOf(this));
    }

    public class AddExpressionNode : BinaryExpressionNode
    {
        internal AddExpressionNode(INode node) : base(node) { }

        public override Expression Expression => Expression.Add(this.Left.Expression, this.Right.Expression);
    }

    public static class Vocabulary
    {
        private const string BaseUri = "http://example.com/";
        private static readonly NodeFactory Factory = new NodeFactory();

        public static IUriNode Add { get; } = EngineNode("Add");

        public static IUriNode Left { get; } = EngineNode("left");

        public static IUriNode Right { get; } = EngineNode("right");

        private static IUriNode EngineNode(string name) => AnyNode($"{BaseUri}{name}");

        private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(UriFactory.Create(uri));
    }

    internal static class Extensions
    {
        internal static IEnumerable<INode> ObjectsOf(this INode predicate, INode subject) =>
            from t in subject.Graph.GetTriplesWithSubjectPredicate(subject, predicate)
            select t.Object;

        internal static INode ObjectOf(this INode predicate, INode subject) =>
            predicate.ObjectsOf(subject).Single();
    }
}
