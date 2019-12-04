﻿namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class TryExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal TryExpressionNode(INode node) : base(node) { }

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public ExpressionNode Body => Vocabulary.Body.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public ExpressionNode Finally => Vocabulary.Finally.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public ExpressionNode Fault => Vocabulary.Fault.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public IEnumerable<CatchBlockNode> Handlers => Vocabulary.Handlers.ObjectsOf(this).SelectMany(Graph.GetListItems).Select(CatchBlockNode.Parse);

        public override Expression Expression => Expression.MakeTry(Type?.Type, Body?.Expression, Finally?.Expression, Fault?.Expression, Handlers.Select(h => h.CatchBlock));
    }
}