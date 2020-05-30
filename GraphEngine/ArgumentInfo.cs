// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using Microsoft.CSharp.RuntimeBinder;
    using VDS.RDF;

    public class ArgumentInfo : Node
    {
        [DebuggerStepThrough]
        public ArgumentInfo(INode node)
            : base(node)
        {
        }

        internal CSharpArgumentInfo Info =>
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);

        internal static ArgumentInfo Parse(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return new ArgumentInfo(node);
        }
    }
}
