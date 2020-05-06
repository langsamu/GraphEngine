// MIT License, Copyright 2019 Samu Lang

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

        public Type? Type => Optional<Type>(SwitchType);

        public Expression SwitchValue => Required<Expression>(SwitchSwitchValue);

        public Expression? DeafultBody => Optional<Expression>(SwitchDefaultBody);

        public Method? Comparison => Optional<Method>(SwitchComparison);

        public IEnumerable<Case> Cases => List<Case>(SwitchCases);

        public override Linq.Expression LinqExpression => Linq.Expression.Switch(this.Type?.SystemType, this.SwitchValue.LinqExpression, this.DeafultBody?.LinqExpression, this.Comparison?.ReflectionMethod, this.Cases.Select(@case => @case.LinqSwitchCase));
    }
}
