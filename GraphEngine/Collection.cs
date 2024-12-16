// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

using System.Collections;

public class Collection(NodeWithGraph subject, INode predicate) : ICollection<NodeWithGraph>
{
    public int Count => !this.IsValid(out var listRoot)
        ? 0
        : subject.Graph.GetListItems(listRoot).Count();

    public bool IsReadOnly => false;

    protected IEnumerable<NodeWithGraph> X => !this.IsValid(out var listRoot)
        ? []
        : subject.Graph.GetListItems(listRoot).Select(n => n.In(subject.Graph));

    public void Add(NodeWithGraph item)
    {
        if (!this.IsValid(out var listRoot))
        {
            subject.Graph.Assert(subject, predicate, subject.Graph.AssertList(item.AsEnumerable()));
            return;
        }

        subject.Graph.AddToList(listRoot, item.AsEnumerable());
    }

    public void Clear()
    {
        if (!this.IsValid(out var listRoot))
        {
            return;
        }

        subject.Graph.RetractList(listRoot);
        subject.Graph.Retract(subject, predicate, listRoot);
    }

    public bool Contains(NodeWithGraph item) => this.IsValid(out var listRoot) && subject.Graph.GetListItems(listRoot).Contains(item);

    public void CopyTo(NodeWithGraph[] array, int arrayIndex)
    {
        if (!this.IsValid(out var listRoot))
        {
            return;
        }

        subject.Graph.GetListItems(listRoot).ToList().CopyTo(array, arrayIndex);
    }

    IEnumerator<NodeWithGraph> IEnumerable<NodeWithGraph>.GetEnumerator() => this.X.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable<INode>)this).GetEnumerator();

    public bool Remove(NodeWithGraph item)
    {
        if (!this.IsValid(out var listRoot))
        {
            return false;
        }

        var contains = subject.Graph.GetListItems(listRoot).Contains(item);

        subject.Graph.RemoveFromList(listRoot, item.AsEnumerable());

        return contains;
    }

    private bool IsValid(out NodeWithGraph? listRoot)
    {
        listRoot = predicate.ObjectOf(subject);

        if (listRoot is null)
        {
            return false;
        }

        if (!listRoot.IsListRoot(subject.Graph))
        {
            throw new Exception("not collection");
        }

        return true;
    }
}
