// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine.Ontology;

using System.Collections.Generic;
using System.Linq;
using VDS.RDF;

public class Graph : WrapperGraph
{
    public IEnumerable<Resource> Ontologies => from o in this.InstancesOf(Vocabulary.OwlOntology) select new Resource(o);

    public IEnumerable<Class> Classes => from o in this.InstancesOf(Vocabulary.OwlClass) select new Class(o);

    public IEnumerable<Property> ObjectProperties => from o in this.InstancesOf(Vocabulary.OwlObjectProperty) select new Property(o);

    public IEnumerable<Property> DatatypeProperties => from o in this.InstancesOf(Vocabulary.OwlDatatypeProperty) select new Property(o);
}
