﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Tests;

using LinqExpression = System.Linq.Expressions.Expression;

public class TestBase
{
    protected static void ShouldBe(string rdf, LinqExpression expected)
    {
        using var g = new GraphEngine.Graph();
        g.LoadFromString(rdf);
        var s = g.GetUriNode(":s").In(g);

        var actual = Expression.Parse(s).LinqExpression;

        Console.WriteLine(actual.GetDebugView());

        actual.Should().Be(expected);
    }
}
