// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Collections;

public class Collection<T>(NodeWithGraph subject, INode predicate, Func<NodeWithGraph, T> parser) : Collection(subject, predicate), ICollection<T>
    where T : NodeWithGraph
{
    public void Add(T item) => base.Add(item);

    public bool Contains(T item) => base.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => this.X.ToList().CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => this.X.Select(parser).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

    public bool Remove(T item) => base.Remove(item);
}
