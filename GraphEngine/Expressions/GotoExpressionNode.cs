namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class GotoExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        public GotoExpressionNode(INode node) : base(node) { }

        protected virtual GotoExpressionKind Kind => GotoExpressionKind.Goto;

        public LabelTargetNode Target => Vocabulary.Target.ObjectsOf(this).Select(LabelTargetNode.Parse).SingleOrDefault();

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public ExpressionNode Value => Vocabulary.Value.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public override Expression Expression => Expression.MakeGoto(Kind, Target?.LabelTarget, Value?.Expression, Type?.Type);
    }
}
