namespace GraphEngine
{
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class TestExtensions
    {
        internal static string GetDebugView(this Expression exp) => typeof(Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(exp) as string;
    }
}
