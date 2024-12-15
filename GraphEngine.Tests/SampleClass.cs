// MIT License, Copyright 2020 Samu Lang

#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable CA1801 // Review unused parameters
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable SA1402 // File may only contain a single type

namespace GraphEngine.Tests;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class SampleClass
{
    public static string StaticField;
    public string InstanceField;
    public SampleClass ComplexField;

    public static string StaticProperty { get; set; }

    public string InstanceProperty { get; set; }

    public List<long> ListProperty { get; }

    [IndexerName("Indexer")]
    public string this[int index]
    {
        get => default;
        set { }
    }

    public static bool Equal(long obj1, long obj2) => throw new NotImplementedException();

    public static void StaticMethod()
    {
    }

    public static void GenericStaticMethod<T>()
    {
    }

    public static void StaticMethodWithArgument(long arg)
    {
    }

    public static bool StaticFunctionWithArgument(bool arg) => default;

    public static void GenericStaticMethodWithArgument<T>(long arg)
    {
    }

    public void InstanceMethod()
    {
    }

    public void GenericInstanceMethod<T>()
    {
    }

    public void InstanceMethodWithArgument(long arg)
    {
    }

    public void GenericInstanceMethodWithArgument<T>(long arg)
    {
    }
}

public class SampleDerivedClass : SampleClass
{
}

#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore CA1801 // Review unused parameters
#pragma warning restore CA2211 // Non-constant fields should not be visible
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore SA1402 // File may only contain a single type
