// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Reflection;

// TODO: Improve derivation
public class Method : Member
{
    [DebuggerStepThrough]
    internal Method(NodeWithGraph node)
        : base(node)
    {
    }

    public ICollection<Type> TypeArguments => this.Collection(MethodTypeArguments, Type.Parse);

    public MethodInfo? ReflectionMethod
    {
        get
        {
            var methodInfo = this.Type.SystemType.GetMethod(this.Name);

            var typeArguments = this.TypeArguments.Select(ta => ta.SystemType).ToArray();
            if (typeArguments.Any())
            {
                methodInfo = methodInfo.MakeGenericMethod(typeArguments);
            }

            return methodInfo;
        }
    }

    internal static new Method Parse(NodeWithGraph node) => node switch
    {
        null => throw new ArgumentNullException(nameof(node)),
        _ => new Method(node)
    };
}
