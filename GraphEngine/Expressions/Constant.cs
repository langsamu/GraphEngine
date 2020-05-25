// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Constant : Expression
    {
        [DebuggerStepThrough]
        internal Constant(INode node)
            : base(node)
        {
        }

        public object? Value
        {
            get => this.GetOptional(ConstantValue, AsObject);

            set => this.SetOptional(ConstantValue, value);
        }

        public Type? Type
        {
            get => this.GetOptional(ConstantType, AsType);

            set => this.SetOptional(ConstantType, value);
        }

        public override Linq.Expression LinqExpression
        {
            get
            {
                if (this.Type is Type type)
                {
                    return Linq.Expression.Constant(this.Value, type.SystemType);
                }

                return Linq.Expression.Constant(this.Value);
            }
        }
    }
}
