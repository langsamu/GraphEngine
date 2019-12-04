namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public abstract class BaseGotoExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        public BaseGotoExpressionNode(INode node) : base(node) { }

        protected abstract GotoExpressionKind Kind { get; }

        public LabelTargetNode Target => Vocabulary.Target.ObjectsOf(this).Select(LabelTargetNode.Parse).SingleOrDefault();

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public ExpressionNode Value => Vocabulary.Value.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public override Expression Expression => Expression.MakeGoto(Kind, Target?.LabelTarget, Value?.Expression, Type?.Type);
    }
}
