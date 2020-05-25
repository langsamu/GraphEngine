﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class Switch : Expression
    {
        [DebuggerStepThrough]
        internal Switch(INode node)
            : base(node)
        {
        }

        public Type? Type
        {
            get => this.GetOptional(SwitchType, AsType);

            set => this.SetOptional(SwitchType, value);
        }

        public Expression SwitchValue
        {
            get => this.GetRequired(SwitchSwitchValue, AsExpression);

            set => this.SetRequired(SwitchSwitchValue, value);
        }

        public Expression? DeafultBody
        {
            get => this.GetOptional(SwitchDefaultBody, AsExpression);

            set => this.SetOptional(SwitchDefaultBody, value);
        }

        public Method? Comparison
        {
            get => this.GetOptional(SwitchComparison, AsMethod);

            set => this.SetOptional(SwitchComparison, value);
        }

        public ICollection<Case> Cases => this.Collection(SwitchCases, AsCase);

        public override Linq.Expression LinqExpression => Linq.Expression.Switch(this.Type?.SystemType, this.SwitchValue.LinqExpression, this.DeafultBody?.LinqExpression, this.Comparison?.ReflectionMethod, this.Cases.Select(@case => @case.LinqSwitchCase));
    }
}
