// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using VDS.RDF;

    public class Collection<T> : Collection, ICollection<T>
        where T : class, INode
    {
        public Collection(INode subject, INode predicate)
            : base(subject, predicate)
        {
        }

        public int Count => base.Count;

        public bool IsReadOnly => base.IsReadOnly;

        public void Add(T item) => base.Add(item);

        public void Clear() => base.Clear();

        public bool Contains(T item) => base.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => this.X.ToList().CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => this.X.Select(Node.Parse<T>).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

        public bool Remove(T item) => base.Remove(item);
    }
}