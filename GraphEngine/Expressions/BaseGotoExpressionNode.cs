// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public abstract class BaseGotoExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        public BaseGotoExpressionNode(INode node)
            : base(node)
        {
        }

        public TargetNode Target => Vocabulary.GotoTarget.ObjectsOf(this).Select(TargetNode.Parse).SingleOrDefault();

        public TypeNode Type => Vocabulary.GotoType.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public ExpressionNode Value => Vocabulary.GotoValue.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public override Expression Expression => Expression.MakeGoto(this.Kind, this.Target?.LabelTarget, this.Value?.Expression, this.Type?.Type);

        protected abstract GotoExpressionKind Kind { get; }
    }
}
