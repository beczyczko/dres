using Dres.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Dres.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddDres(this IServiceCollection services, Action<DresOptions> options)
    {
        var dresOptions = new DresOptions();
        options(dresOptions);
        
        services.AddSingleton(dresOptions);
        services.AddTransient<IResourcesProvider, ResourcesProvider>();
        services.AddTransient<IResourceRelationsPumlBuilder, ResourceRelationsPumlBuilder>();
        services.AddTransient<IAssembliesResolver, AssembliesResolver>();
        services.AddTransient<ISpecificationProvider, SpecificationProvider>();
    }
}