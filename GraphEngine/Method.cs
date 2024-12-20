// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Reflection;

// TODO: Improve derivation
public class Method(NodeWithGraph node) : Member(node)
{
    public ICollection<Type> TypeArguments => Collection(MethodTypeArguments, Type.Parse);

    public MethodInfo? ReflectionMethod
    {
        get
        {
            var methodInfo = Type.SystemType.GetMethod(Name);

            var typeArguments = TypeArguments.Select(ta => ta.SystemType).ToArray();
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
