// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class DefaultExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal DefaultExpressionNode(INode node)
            : base(node)
        {
        }

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).Single();

        public override Expression Expression => Expression.Default(this.Type.Type);
    }
}
