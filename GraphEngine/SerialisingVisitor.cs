// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using VDS.RDF;
    using Linq = System.Linq.Expressions;

    public class SerialisingVisitor : Linq.ExpressionVisitor
    {
        private readonly IDictionary<object, INode> mapping = new Dictionary<object, INode>();
        private readonly INode node;
        private readonly Stack<INode> path = new Stack<INode>();
        private bool initialised;

        public SerialisingVisitor(INode node)
          : base()
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
        }

        private INode Current => this.path.Peek();

        private INode this[object index]
        {
            get
            {
                if (!this.initialised)
                {
                    this.mapping[index] = this.node;
                    this.initialised = true;

                    return this.node;
                }

                if (!this.mapping.TryGetValue(index, out var current))
                {
                    current = this.mapping[index] = this.node.Graph.CreateBlankNode();
                }

                return current;
            }
        }

        public override Linq.Expression Visit(Linq.Expression node)
        {
            using (this.Wrap(node))
            {
                return base.Visit(node);
            }
        }

        protected override Linq.Expression VisitBinary(Linq.BinaryExpression node)
        {
            var binary = node.NodeType switch
            {
                Linq.ExpressionType.Add => Add.Create(this.Current) as Binary,
                Linq.ExpressionType.Assign => Assign.Create(this.Current) as Binary,
                Linq.ExpressionType.GreaterThan => GreaterThan.Create(this.Current) as Binary,
                Linq.ExpressionType.MultiplyAssign => MultiplyAssign.Create(this.Current) as Binary,
            };

            binary.Left = this.VisitCacheParse(node.Left);
            binary.Right = this.VisitCacheParse(node.Right);

            return node;
        }

        protected override Linq.Expression VisitBlock(Linq.BlockExpression node)
        {
            var block = Block.Create(this.Current);

            foreach (var variable in node.Variables)
            {
                block.Variables.Add(new Parameter(this.VisitCache(variable)));
            }

            foreach (var blockExpression in node.Expressions)
            {
                block.Expressions.Add(this.VisitCacheParse(blockExpression));
            }

            return node;
        }

        protected override Linq.Expression VisitConditional(Linq.ConditionalExpression node)
        {
            Condition condition;

            if (node.Type == typeof(void))
            {
                if (node.IfFalse is Linq.DefaultExpression defaultExpression && defaultExpression.Type == typeof(void))
                {
                    condition = IfThen.Create(this.Current);
                }
                else
                {
                    condition = IfThenElse.Create(this.Current);
                    condition.IfFalse = this.VisitCacheParse(node.IfFalse);
                }
            }
            else
            {
                condition = Condition.Create(this.Current);
                condition.IfFalse = this.VisitCacheParse(node.IfFalse);

                if (node.Type != node.IfTrue.Type)
                {
                    condition.Type = this.VisitType(node.Type);
                }
            }

            condition.Test = this.VisitCacheParse(node.Test);
            condition.IfTrue = this.VisitCacheParse(node.IfTrue);

            return node;
        }

        protected override Linq.Expression VisitConstant(Linq.ConstantExpression node)
        {
            var constant = Constant.Create(this.Current);

            constant.Value = node.Value;

            return base.VisitConstant(node);
        }

        protected override Linq.Expression VisitDefault(Linq.DefaultExpression node)
        {
            var @default = Default.Create(this.Current);

            @default.Type = this.VisitType(node.Type);

            return node;
        }

        protected override Linq.Expression VisitGoto(Linq.GotoExpression node)
        {
            var @goto = node.Kind switch
            {
                Linq.GotoExpressionKind.Goto => Goto.Create(this.Current) as BaseGoto,
                Linq.GotoExpressionKind.Return => Return.Create(this.Current) as BaseGoto,
                Linq.GotoExpressionKind.Break => Break.Create(this.Current) as BaseGoto,
                Linq.GotoExpressionKind.Continue => Continue.Create(this.Current) as BaseGoto,
            };

            if (node.Value is object)
            {
                @goto.Value = this.VisitCacheParse(node.Value);
            }

            if (node.Type != typeof(void))
            {
                @goto.Type = this.VisitType(node.Type);
            }

            @goto.Target = new Target(this[this.VisitLabelTarget(node.Target)]);

            return node;
        }

        protected override Linq.LabelTarget VisitLabelTarget(Linq.LabelTarget node)
        {
            using (this.Wrap(node))
            {
                var target = Target.Create(this.Current);

                if (node.Type != typeof(void))
                {
                    target.Type = this.VisitType(node.Type);
                }

                if (node.Name is object)
                {
                    target.Name = node.Name;
                }

                return node;
            }
        }

        protected override Linq.Expression VisitLoop(Linq.LoopExpression node)
        {
            var loop = Loop.Create(this.Current);
            loop.Body = this.VisitCacheParse(node.Body);

            if (node.ContinueLabel is object)
            {
                loop.Continue = new Target(this[this.VisitLabelTarget(node.ContinueLabel)]);
            }

            if (node.BreakLabel is object)
            {
                loop.Break = new Target(this[this.VisitLabelTarget(node.BreakLabel)]);
            }

            return node;
        }

        protected override Linq.Expression VisitParameter(Linq.ParameterExpression node)
        {
            var parameter = Parameter.Create(this.Current);

            parameter.Type = this.VisitType(node.Type);

            if (node.Name is object)
            {
                parameter.Name = node.Name;
            }

            return node;
        }

        protected override Linq.Expression VisitUnary(Linq.UnaryExpression node)
        {
            var unary = node.NodeType switch
            {
                Linq.ExpressionType.PostDecrementAssign => PostDecrementAssign.Create(this.Current) as Unary,
                Linq.ExpressionType.ArrayLength => ArrayLength.Create(this.Current) as Unary,
            };

            unary.Type = this.VisitType(node.Type);

            unary.Operand = this.VisitCacheParse(node.Operand);

            return node;
        }

        private Expression VisitCacheParse(Linq.Expression node) => Expression.Parse(this.VisitCache(node));

        private INode VisitCache(Linq.Expression node) => this[this.Visit(node)];

        private Type VisitType(System.Type type)
        {
            using (this.Wrap(type))
            {
                var t = Type.Create(this.Current);
                t.Name = $"{type.Namespace}.{type.Name}";

                return t;
            }
        }

        private IDisposable Wrap(object node)
        {
            return new Wrapper(this, this[node]);
        }

        private struct Wrapper : IDisposable
        {
            private readonly SerialisingVisitor visitor;

            internal Wrapper(SerialisingVisitor visitor, INode node)
            {
                this.visitor = visitor;
                this.visitor.path.Push(node);
            }

            void IDisposable.Dispose() => this.visitor.path.Pop();
        }
    }
}
