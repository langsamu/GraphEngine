// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using VDS.RDF;

    public class Collection : ICollection<INode>
    {
        private readonly INode subject;
        private readonly INode predicate;

        public Collection(INode subject, INode predicate)
        {
            this.subject = subject;
            this.predicate = predicate;
        }

        public int Count
        {
            get
            {
                if (!this.IsValid(out var listRoot))
                {
                    return 0;
                }

                return this.subject.Graph.GetListItems(listRoot).Count();
            }
        }

        public bool IsReadOnly => false;

        protected IEnumerable<INode> X
        {
            get
            {
                if (!this.IsValid(out var listRoot))
                {
                    return Enumerable.Empty<INode>();
                }

                return this.subject.Graph.GetListItems(listRoot);
            }
        }

        public void Add(INode item)
        {
            if (!this.IsValid(out var listRoot))
            {
                this.subject.Graph.Assert(this.subject, this.predicate, this.subject.Graph.AssertList(item.AsEnumerable()));
                return;
            }

            this.subject.Graph.AddToList(listRoot, item.AsEnumerable());
        }

        public void Clear()
        {
            if (!this.IsValid(out var listRoot))
            {
                return;
            }

            this.subject.Graph.RetractList(listRoot);
            this.subject.Graph.Retract(this.subject, this.predicate, listRoot);
        }

        public bool Contains(INode item)
        {
            if (!this.IsValid(out var listRoot))
            {
                return false;
            }

            return this.subject.Graph.GetListItems(listRoot).Contains(item);
        }

        public void CopyTo(INode[] array, int arrayIndex)
        {
            if (!this.IsValid(out var listRoot))
            {
                return;
            }

            this.subject.Graph.GetListItems(listRoot).ToList().CopyTo(array, arrayIndex);
        }

        IEnumerator<INode> IEnumerable<INode>.GetEnumerator() => this.X.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            ((IEnumerable<INode>)this).GetEnumerator();

        public bool Remove(INode item)
        {
            if (!this.IsValid(out var listRoot))
            {
                return false;
            }

            var contains = this.subject.Graph.GetListItems(listRoot).Contains(item);

            this.subject.Graph.RemoveFromList(listRoot, item.AsEnumerable());

            return contains;
        }

        private bool IsValid(out INode? listRoot)
        {
            listRoot = this.predicate.ObjectOf(this.subject);

            if (listRoot is null)
            {
                return false;
            }

            if (!listRoot.IsListRoot(this.subject.Graph))
            {
                throw new Exception("not collection");
            }

            return true;
        }
    }
}
