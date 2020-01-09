// MIT License, Copyright 2019 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VDS.RDF;
    using VDS.RDF.Nodes;
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

        protected INode Current => this.path.Peek();

        public override Linq.Expression Visit(Linq.Expression node)
        {
            if (node is object)
            {
                using (this.Wrap(this.Lookup(node)))
                {
                    return base.Visit(node);
                }
            }

            return null;
        }

        protected override Linq.Expression VisitBinary(Linq.BinaryExpression node)
        {
            this.AddType(node.NodeType.AsNode());
            this.AddStatement(Vocabulary.BinaryLeft, node.Left);
            this.AddStatement(Vocabulary.BinaryRight, node.Right);

            return base.VisitBinary(node);
        }

        protected override Linq.Expression VisitBlock(Linq.BlockExpression node)
        {
            this.AddType(Vocabulary.Block);

            if (node.Variables.Any())
            {
                this.AddList(Vocabulary.BlockVariables, node.Variables.Select(this.Lookup));
            }

            if (node.Expressions.Any())
            {
                this.AddList(Vocabulary.BlockExpressions, node.Expressions.Select(this.Lookup));
            }

            return base.VisitBlock(node);
        }

        protected override Linq.Expression VisitConditional(Linq.ConditionalExpression node)
        {
            this.AddStatement(Vocabulary.ConditionTest, node.Test);
            this.AddStatement(Vocabulary.ConditionIfTrue, node.IfTrue);

            if (node.Type == typeof(void))
            {
                if (node.IfFalse is Linq.DefaultExpression defaultExpression && defaultExpression.Type == typeof(void))
                {
                    this.AddType(Vocabulary.IfThen);
                }
                else
                {
                    this.AddType(Vocabulary.IfThenElse);
                    this.AddStatement(Vocabulary.ConditionIfFalse, node.IfFalse);
                }
            }
            else
            {
                this.AddType(Vocabulary.Condition);
                this.AddStatement(Vocabulary.ConditionIfFalse, node.IfFalse);

                if (node.IfTrue.Type != node.IfFalse.Type)
                {
                    this.VisitType(node.Type, Vocabulary.ConditionType);
                }
            }

            return base.VisitConditional(node);
        }

        protected override Linq.Expression VisitConstant(Linq.ConstantExpression node)
        {
            this.AddType(Vocabulary.Constant);
            // TODO: currentNode.Graph.Assert(currentNode, Vocabulary.ConstantValue, this.Lookup(node.Value));
            this.VisitType(node.Type, Vocabulary.ConstantType);

            return base.VisitConstant(node);
        }

        protected override Linq.Expression VisitGoto(Linq.GotoExpression node)
        {
            this.AddType(node.Kind.AsNode());

            if (node.Value is object)
            {
                this.AddStatement(Vocabulary.GotoValue, node.Value);
            }

            this.VisitType(node.Type, Vocabulary.GotoType);

            var targetNode = this.Lookup(node.Target);
            this.AddStatement(Vocabulary.GotoTarget, targetNode);
            using (this.Wrap(targetNode))
            {
                this.VisitLabelTarget(node.Target);
            }

            this.Visit(node.Value);

            return node;
        }

        protected override Linq.LabelTarget VisitLabelTarget(Linq.LabelTarget node)
        {
            this.AddStatement(Vocabulary.TargetName, node.Name);
            this.VisitType(node.Type, Vocabulary.TargetType);

            return base.VisitLabelTarget(node);
        }

        protected override Linq.Expression VisitLoop(Linq.LoopExpression node)
        {
            this.AddType(Vocabulary.Loop);
            this.AddStatement(Vocabulary.LoopBody, node.Body);
            this.VisitLabel(Vocabulary.LoopBreak, node.BreakLabel);
            this.VisitLabel(Vocabulary.LoopContinue, node.ContinueLabel);
            this.Visit(node.Body);

            return node;
        }

        protected override Linq.Expression VisitParameter(Linq.ParameterExpression node)
        {
            this.AddType(Vocabulary.Parameter);
            this.AddStatement(Vocabulary.ParameterName, node.Name);
            this.VisitType(node.Type, Vocabulary.ParameterType);

            return base.VisitParameter(node);
        }

        protected override Linq.Expression VisitUnary(Linq.UnaryExpression node)
        {
            this.AddType(node.NodeType.AsNode());
            this.AddStatement(Vocabulary.UnaryOperand, node.Operand);
            this.VisitType(node.Type, Vocabulary.UnaryType);

            return base.VisitUnary(node);
        }

        private void AddStatement(INode p, Linq.Expression e)
        {
            this.AddStatement(p, this.Lookup(e));
        }

        private void AddStatement(INode p, string o)
        {
            if (o is object)
            {
                this.AddStatement(p, new StringNode(this.Current.Graph, o));
            }
        }

        private void AddStatement(INode p, INode o)
        {
            this.Current.Graph.Assert(this.Current, p, o);
        }

        private void AddList(INode predicate, IEnumerable<INode> nodes)
        {
            var root = this.Current.Graph.AssertList(nodes);
            this.AddStatement(predicate, root);
        }

        private void AddType(INode type)
        {
            this.AddStatement(Vocabulary.RdfType, type);
        }

        private INode Lookup(object o)
        {
            if (!this.initialised)
            {
                this.mapping[o] = this.node;
                this.initialised = true;

                return this.node;
            }

            if (!this.mapping.TryGetValue(o, out var current))
            {
                current = this.mapping[o] = this.node.Graph.CreateBlankNode();
            }

            return current;
        }

        private void VisitLabel(INode predicate, Linq.LabelTarget label)
        {
            if (label is object)
            {
                var labelNode = this.Lookup(label);
                this.AddStatement(predicate, labelNode);

                using (this.Wrap(labelNode))
                {
                    this.VisitLabelTarget(label);
                }
            }
        }

        private void VisitType(System.Type type)
        {
            // TODO: What about assembly name?
            this.AddStatement(Vocabulary.TypeName, $"{type.Namespace}.{type.Name}");

            if (type.GenericTypeArguments.Any())
            {
                var nodes = new List<INode>();

                foreach (var typeArgument in type.GenericTypeArguments)
                {
                    var typeArgumentNode = this.Lookup(typeArgument);
                    nodes.Add(typeArgumentNode);

                    using (this.Wrap(typeArgumentNode))
                    {
                        this.VisitType(typeArgument);
                    }
                }

                this.AddList(Vocabulary.TypeArguments, nodes);
            }
        }

        private void VisitType(System.Type type, INode predicate)
        {
            //if (type is object)
            //{
            var typeNode = this.Lookup(type);
            this.AddStatement(predicate, typeNode);

            using (this.Wrap(typeNode))
            {
                this.VisitType(type);
            }
            //}
        }

        private IDisposable Wrap(INode node)
        {
            return new Wrapper(this, node);
        }

        private class Wrapper : IDisposable
        {
            private readonly SerialisingVisitor visitor;

            public Wrapper(SerialisingVisitor visitor, INode node)
            {
                this.visitor = visitor;
                this.visitor.path.Push(node);
            }

            void IDisposable.Dispose() => this.visitor.path.Pop();
        }
    }
}
