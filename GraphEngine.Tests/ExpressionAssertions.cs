// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using FluentAssertions;
    using FluentAssertions.Execution;
    using FluentAssertions.Primitives;
    using LinqExpression = System.Linq.Expressions.Expression;

    internal class ExpressionAssertions : ReferenceTypeAssertions<LinqExpression, ExpressionAssertions>
    {
        internal ExpressionAssertions(LinqExpression expression)
        {
            this.Subject = expression;
        }

        protected override string Identifier => "expression";

        internal AndConstraint<ExpressionAssertions> Be(LinqExpression expected, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(ExpressionTreeToolkit.ExpressionEqualityComparer.Default.Equals(this.Subject, expected))
                .FailWith("Expected {context:expression} to be equal to {0}{reason}, but {1} was not.", expected, this.Subject);

            return new AndConstraint<ExpressionAssertions>(this);
        }
    }
}
