namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using VDS.RDF;

    public class CatchBlockNode : WrapperNode
    {
        [DebuggerStepThrough]
        public CatchBlockNode(INode node) : base(node) { }

        public TypeNode Type => Vocabulary.Type.ObjectsOf(this).Select(TypeNode.Parse).SingleOrDefault();

        public ExpressionNode Body => Vocabulary.Body.ObjectsOf(this).Select(ExpressionNode.Parse).Single();

        public ParameterExpressionNode Variable => Vocabulary.CatchVariable.ObjectsOf(this).Select(ParameterExpressionNode.Parse).SingleOrDefault();

        public ExpressionNode Filter => Vocabulary.Filter.ObjectsOf(this).Select(ExpressionNode.Parse).SingleOrDefault();

        public CatchBlock CatchBlock
        {
            get
            {
                var variable = Variable?.Parameter;
                return Expression.MakeCatchBlock(Type?.Type ?? variable.Type, variable, Body.Expression, Filter?.Expression);
            }
        }

        internal static CatchBlockNode Parse(INode node) => new CatchBlockNode(node);
    }
}