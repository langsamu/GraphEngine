﻿// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using VDS.RDF;

    public class Collection<T> : Collection, ICollection<T>
        where T : class, INode
    {
        private readonly Func<INode, T> parser;

        public Collection(INode subject, INode predicate, Func<INode, T> parser)
            : base(subject, predicate)
        {
            this.parser = parser;
        }

        public void Add(T item) => base.Add(item);

        public bool Contains(T item) => base.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => this.X.ToList().CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => this.X.Select(this.parser).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

        public bool Remove(T item) => base.Remove(item);
    }
}
