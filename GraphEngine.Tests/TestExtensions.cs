// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System.Reflection;
    using Linq = System.Linq.Expressions;

    internal static class TestExtensions
    {
        internal static string GetDebugView(this Linq.Expression exp) => (string)typeof(Linq.Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(exp);
    }
}
