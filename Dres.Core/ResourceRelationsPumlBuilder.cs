using System.Text;

namespace Dres.Core;

public class ResourceRelationsPumlBuilder : IResourceRelationsPumlBuilder
{
    public string Build(IEnumerable<Resource> resources)
    {
        var resourcesAsList = resources.ToList();

        var domainContexts = resourcesAsList
            .GroupBy(
                r => r.DomainContext,
                (key, res) => new DomainContext(key, res));

        var relations = resourcesAsList
            .SelectMany(r => r.Properties)
            .Where(property => property.RelatedResourcesIdentifiers.Any())
            .SelectMany(
                property => property.RelatedResourcesIdentifiers.Select(
                    relatedResource => new Relation(
                        property,
                        relatedResource,
                        resourcesAsList.Find(r => r.Identifier == relatedResource))))
            .ToList();

        var puml = new StringBuilder()
            .AppendLine(value: "@startuml")
            .AppendLine()
            .AppendLine(value: "left to right direction")
            .AppendLine(value: "title Domain model relationships")
            .AppendLine()
            .AppendDomainContexts(domainContexts, baseIndent: 0)
            .AppendLine()
            .AppendRelations(relations, baseIndent: 0)
            .AppendLine()
            .AppendLine(value: "@enduml")
            .ToString();

        return puml;
    }
}