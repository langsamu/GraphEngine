namespace GraphEngine
{
    using System.Collections.Generic;
    using System.Linq;
    using VDS.RDF;

    internal static class Extensions
    {
        internal static IEnumerable<INode> ObjectsOf(this INode predicate, INode subject) =>
            from t in subject.Graph.GetTriplesWithSubjectPredicate(subject, predicate)
            select t.Object;

        internal static INode ObjectOf(this INode predicate, INode subject) =>
            predicate.ObjectsOf(subject).Single();
    }
}
