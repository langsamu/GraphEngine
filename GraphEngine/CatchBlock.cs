// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class CatchBlock : WrapperNode
    {
        [DebuggerStepThrough]
        public CatchBlock(INode node)
            : base(node)
        {
        }

        public Type Type => Vocabulary.CatchType.ObjectsOf(this).Select(Type.Parse).SingleOrDefault();

        public Expression Body => Vocabulary.CatchBody.ObjectsOf(this).Select(Expression.Parse).Single();

        public Parameter Variable => Vocabulary.CatchVariable.ObjectsOf(this).Select(Parameter.Parse).SingleOrDefault();

        public Expression Filter => Vocabulary.CatchFilter.ObjectsOf(this).Select(Expression.Parse).SingleOrDefault();

        public Linq.CatchBlock LinqCatchBlock
        {
            get
            {
                var variable = this.Variable?.LinqParameter;
                return Linq.Expression.MakeCatchBlock(this.Type?.SystemType ?? variable.Type, variable, this.Body.LinqExpression, this.Filter?.LinqExpression);
            }
        }

        internal static CatchBlock Parse(INode node) => new CatchBlock(node);
    }
}