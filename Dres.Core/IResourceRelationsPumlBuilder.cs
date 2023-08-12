namespace Dres.Core;

public interface IResourceRelationsPumlBuilder
{
    string Build(IEnumerable<Resource> resources);
}