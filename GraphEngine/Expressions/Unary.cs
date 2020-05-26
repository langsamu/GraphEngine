// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public abstract class Unary : Expression
    {
        [DebuggerStepThrough]
        protected Unary(INode node)
            : base(node)
        {
        }

        public Expression Operand
        {
            get => this.GetRequired(UnaryOperand, Expression.Parse);

            set => this.SetRequired(UnaryOperand, value);
        }

        public Type? Type
        {
            get => this.GetOptional(UnaryType, Type.Parse);

            set => this.SetOptional(UnaryType, value);
        }

        public override Linq.Expression LinqExpression => Linq.Expression.MakeUnary(this.LinqUnaryType, this.Operand.LinqExpression, this.Type?.SystemType);

        protected abstract Linq.ExpressionType LinqUnaryType { get; }

        public static Unary Create(INode node, Linq.ExpressionType type)
        {
            return type switch
            {
                Linq.ExpressionType.Negate => new Negate(node),
                Linq.ExpressionType.NegateChecked => new NegateChecked(node),
                Linq.ExpressionType.Not => new Not(node),
                Linq.ExpressionType.IsFalse => new IsFalse(node),
                Linq.ExpressionType.IsTrue => new IsTrue(node),
                Linq.ExpressionType.OnesComplement => new OnesComplement(node),
                Linq.ExpressionType.ArrayLength => new ArrayLength(node),
                Linq.ExpressionType.Convert => new Convert(node),
                Linq.ExpressionType.ConvertChecked => new ConvertChecked(node),
                Linq.ExpressionType.Throw => new Throw(node),
                Linq.ExpressionType.TypeAs => new TypeAs(node),
                Linq.ExpressionType.Quote => new Quote(node),
                Linq.ExpressionType.UnaryPlus => new UnaryPlus(node),
                Linq.ExpressionType.Unbox => new Unbox(node),
                Linq.ExpressionType.Increment => new Increment(node),
                Linq.ExpressionType.Decrement => new Decrement(node),
                Linq.ExpressionType.PreIncrementAssign => new PreIncrementAssign(node),
                Linq.ExpressionType.PostIncrementAssign => new PostIncrementAssign(node),
                Linq.ExpressionType.PreDecrementAssign => new PreDecrementAssign(node),
                Linq.ExpressionType.PostDecrementAssign => new PostDecrementAssign(node),

                _ => throw new System.Exception("{type} is not unary")
            };
        }
    }
}
