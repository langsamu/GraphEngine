// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Binary : Expression
    {
        [DebuggerStepThrough]
        public Binary(INode node)
            : base(node)
        {
        }

        public Expression Left
        {
            get => this.GetRequired(BinaryLeft, Expression.Parse);

            set => this.SetRequired(BinaryLeft, value);
        }

        public Expression Right
        {
            get => this.GetRequired(BinaryRight, Expression.Parse);

            set => this.SetRequired(BinaryRight, value);
        }

        public ExpressionType ExpressionType
        {
            get => this.GetRequired(BinaryExpressionType, ExpressionType.Parse);

            set => this.SetRequired(BinaryExpressionType, value);
        }

        public override Linq.Expression LinqExpression => Linq.Expression.MakeBinary(this.ExpressionType.LinqExpressionType, this.Left.LinqExpression, this.Right.LinqExpression);

        public static Binary Create(INode node, Linq.ExpressionType type)
        {
            switch (type)
            {
                case Linq.ExpressionType.Add:
                case Linq.ExpressionType.AddAssign:
                case Linq.ExpressionType.AddAssignChecked:
                case Linq.ExpressionType.AddChecked:
                case Linq.ExpressionType.And:
                case Linq.ExpressionType.AndAlso:
                case Linq.ExpressionType.AndAssign:
                case Linq.ExpressionType.Assign:
                case Linq.ExpressionType.Coalesce:
                case Linq.ExpressionType.Divide:
                case Linq.ExpressionType.DivideAssign:
                case Linq.ExpressionType.Equal:
                case Linq.ExpressionType.ExclusiveOr:
                case Linq.ExpressionType.ExclusiveOrAssign:
                case Linq.ExpressionType.GreaterThan:
                case Linq.ExpressionType.GreaterThanOrEqual:
                case Linq.ExpressionType.LeftShift:
                case Linq.ExpressionType.LeftShiftAssign:
                case Linq.ExpressionType.LessThan:
                case Linq.ExpressionType.LessThanOrEqual:
                case Linq.ExpressionType.Modulo:
                case Linq.ExpressionType.ModuloAssign:
                case Linq.ExpressionType.Multiply:
                case Linq.ExpressionType.MultiplyAssign:
                case Linq.ExpressionType.MultiplyAssignChecked:
                case Linq.ExpressionType.MultiplyChecked:
                case Linq.ExpressionType.NotEqual:
                case Linq.ExpressionType.Or:
                case Linq.ExpressionType.OrAssign:
                case Linq.ExpressionType.OrElse:
                case Linq.ExpressionType.Power:
                case Linq.ExpressionType.PowerAssign:
                case Linq.ExpressionType.RightShift:
                case Linq.ExpressionType.RightShiftAssign:
                case Linq.ExpressionType.Subtract:
                case Linq.ExpressionType.SubtractAssign:
                case Linq.ExpressionType.SubtractAssignChecked:
                case Linq.ExpressionType.SubtractChecked:
                    return new Binary(node)
                    {
                        ExpressionType = ExpressionType.Create(type),
                    };

                default:
                    throw new Exception($"{type} is not binary");
            }
        }
    }
}
