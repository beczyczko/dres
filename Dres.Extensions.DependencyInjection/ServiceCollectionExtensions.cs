using Dres.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dres.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddDres(this IServiceCollection services, Action<DresOptions> options)
    {
        var dresOptions = new DresOptions();
        options(dresOptions);

        services.AddTransient<IResourcesProvider, ResourcesProvider>();
        services.AddTransient<IResourceRelationsPumlBuilder, ResourceRelationsPumlBuilder>();
        services.AddSingleton<IAssembliesResolver>(_ => new AssembliesResolver(dresOptions.AssembliesGetFunc));

        services.AddControllers().AddApplicationPart(typeof(ResourcesController).Assembly);
    }
}