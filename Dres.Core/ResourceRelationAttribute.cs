namespace Dres.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ResourceRelationAttribute : Attribute
{
    public ResourceRelationAttribute(params string[] resourceIdentifiers)
    {
        ResourceIdentifiers = resourceIdentifiers;
    }

    public string[] ResourceIdentifiers { get; }
}