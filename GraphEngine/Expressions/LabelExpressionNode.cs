// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class LabelExpressionNode : ExpressionNode
    {
        [DebuggerStepThrough]
        internal LabelExpressionNode(INode node)
            : base(node)
        {
        }

        public TargetNode Target => Vocabulary.LabelTarget.ObjectsOf(this).Select(TargetNode.Parse).Single();

        public ExpressionNode DefaultValue => Vocabulary.LabelDefaultValue.ObjectsOf(this).Select(Parse).SingleOrDefault();

        public override Expression Expression => Expression.Label(this.Target.LabelTarget, this.DefaultValue?.Expression);
    }
}
