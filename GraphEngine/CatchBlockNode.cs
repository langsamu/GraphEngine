// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class CatchBlockNode : WrapperNode
    {
        [DebuggerStepThrough]
        public CatchBlockNode(INode node)
            : base(node)
        {
        }

        public TypeNode Type => Vocabulary.CatchType.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public ExpressionNode Body => Vocabulary.CatchBody.ObjectsOf(this).Select(ExpressionNode.Parse).Single();

        public ParameterExpressionNode Variable => Vocabulary.CatchVariable.ObjectsOf(this).Select(ParameterExpressionNode.Parse).SingleOrDefault();

        public ExpressionNode Filter => Vocabulary.CatchFilter.ObjectsOf(this).Select(ExpressionNode.Parse).SingleOrDefault();

        public CatchBlock CatchBlock
        {
            get
            {
                var variable = this.Variable?.Parameter;
                return Expression.MakeCatchBlock(this.Type?.Type ?? variable.Type, variable, this.Body.Expression, this.Filter?.Expression);
            }
        }

        internal static CatchBlockNode Parse(INode node) => new CatchBlockNode(node);
    }
}