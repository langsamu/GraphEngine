// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests
{
    using System.Reflection;
    using Linq = System.Linq.Expressions;

    internal static class TestExtensions
    {
        internal static string GetDebugView(this Linq.Expression exp) => (string)typeof(Linq.Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(exp);

        /* 
         * TODO: Replcae invocations 
         * with `actual.Should().BeEquivalentTo(expected, options => options.Using(ExpressionTreeToolkit.ExpressionEqualityComparer.Default));`
         * once available
         * see https://github.com/fluentassertions/fluentassertions/blob/c192ca014ae920c96f76a747b72bf110c31ba153/Src/FluentAssertions/Equivalency/SelfReferenceEquivalencyAssertionOptions.cs
         * and https://github.com/fluentassertions/fluentassertions/blob/c7ec7c6d1603478566c09062d90dcc2433fdf534/Src/FluentAssertions/Equivalency/EqualityComparerEquivalencyStep.cs
         */
        internal static ExpressionAssertions Should(this Linq.Expression expression) => new ExpressionAssertions(expression);
    }
}
