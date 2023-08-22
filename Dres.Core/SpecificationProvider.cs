namespace Dres.Core;

public class SpecificationProvider : ISpecificationProvider
{
    private readonly DresOptions _dresOptions;
    private readonly IResourcesProvider _resourcesProvider;
    private readonly IAssembliesResolver _assembliesResolver;

    public SpecificationProvider(
        IResourcesProvider resourcesProvider,
        IAssembliesResolver assembliesResolver,
        DresOptions dresOptions)
    {
        _resourcesProvider = resourcesProvider;
        _assembliesResolver = assembliesResolver;
        _dresOptions = dresOptions;
    }

    public Specification Get()
    {
        var assemblies = _assembliesResolver.GetAvailable();
        var resources = _resourcesProvider.Get(assemblies);

        return new Specification(
            _dresOptions.SpecificationName,
            _dresOptions.SpecificationTag,
            DresOptions.DresApiVersion,
            resources.ToList());
    }
}