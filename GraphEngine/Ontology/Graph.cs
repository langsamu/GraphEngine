// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Ontology
{
    using System.Collections.Generic;
    using System.Linq;
    using VDS.RDF;

    public class Graph : WrapperGraph
    {
        public IEnumerable<Resource> Ontologies => this.InstancesOf(Vocabulary.OwlOntology).Select(o => new Resource(o));

        public IEnumerable<Class> Classes => this.InstancesOf(Vocabulary.OwlClass).Select(o => new Class(o));

        public IEnumerable<Property> ObjectProperties => this.InstancesOf(Vocabulary.OwlObjectProperty).Select(o => new Property(o));

        public IEnumerable<Property> DatatypeProperties => this.InstancesOf(Vocabulary.OwlDatatypeProperty).Select(o => new Property(o));
    }
}
