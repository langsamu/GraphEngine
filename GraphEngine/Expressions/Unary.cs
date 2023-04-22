// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Unary : Expression
    {
        [DebuggerStepThrough]
        public Unary(NodeWithGraph node)
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

        public ExpressionType ExpressionType
        {
            get => this.GetRequired(UnaryExpressionType, ExpressionType.Parse);

            set => this.SetRequired(UnaryExpressionType, value);
        }

        public Method? Method
        {
            get => this.GetOptional(UnaryMethod, Method.Parse);

            set => this.SetOptional(UnaryMethod, value);
        }

        public override Linq.Expression LinqExpression =>
            Linq.Expression.MakeUnary(
                this.ExpressionType.LinqExpressionType,
                this.Operand.LinqExpression,
                this.Type?.SystemType,
                this.Method?.ReflectionMethod);

        public static Unary Create(NodeWithGraph node, Linq.ExpressionType type)
        {
            switch (type)
            {
                case Linq.ExpressionType.ArrayLength:
                case Linq.ExpressionType.Convert:
                case Linq.ExpressionType.ConvertChecked:
                case Linq.ExpressionType.Decrement:
                case Linq.ExpressionType.Increment:
                case Linq.ExpressionType.IsFalse:
                case Linq.ExpressionType.IsTrue:
                case Linq.ExpressionType.Negate:
                case Linq.ExpressionType.NegateChecked:
                case Linq.ExpressionType.Not:
                case Linq.ExpressionType.OnesComplement:
                case Linq.ExpressionType.PostDecrementAssign:
                case Linq.ExpressionType.PostIncrementAssign:
                case Linq.ExpressionType.PreDecrementAssign:
                case Linq.ExpressionType.PreIncrementAssign:
                case Linq.ExpressionType.Quote:
                case Linq.ExpressionType.TypeAs:
                case Linq.ExpressionType.UnaryPlus:
                case Linq.ExpressionType.Unbox:
                    return new Unary(node)
                    {
                        ExpressionType = ExpressionType.Create(type, node.Graph),
                    };

                default:
                    throw new Exception($"{type} is not unary");
            }
        }
    }
}
