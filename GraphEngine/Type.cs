﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;

    public class Type : Node
    {
        [DebuggerStepThrough]
        public Type(INode node)
            : base(node)
        {
        }

        public string Name
        {
            get => this.GetRequired(TypeName, AsString);

            set => this.SetRequired(TypeName, value);
        }

        public ICollection<Type> Arguments => this.Collection(TypeArguments, Parse);

        public System.Type SystemType
        {
            get
            {
                var t = System.Type.GetType(this.Name) ?? throw new InvalidOperationException($"Type {this.Name} not found.");

                if (t.IsGenericTypeDefinition)
                {
                    return t.MakeGenericType(this.Arguments.Select(arg => arg.SystemType).ToArray());
                }

                return t;
            }
        }

        internal static Type Parse(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return new Type(node);
        }
    }
}
