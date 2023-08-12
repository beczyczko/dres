using System.Collections.Immutable;
using Dres.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Dres.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddDres(this IServiceCollection services, Action<DresOptions> options)
    {
        var dresOptions = new DresOptions();
        options(dresOptions);

        // Other service registrations
        services.AddTransient<IResourcesProvider, ResourcesProvider>();
        services.AddTransient<IResourceRelationsPumlBuilder, ResourceRelationsPumlBuilder>();
        services.AddSingleton<IAssembliesResolver>(_ => new AssembliesResolver(dresOptions.AssembliesGetFunc));

        // Register the controllers from your library
        services.AddControllers().AddApplicationPart(typeof(ResourcesController).Assembly);
    }

    public static void UseDres(this IApplicationBuilder app)
    {
        // app.MapControllerRoute(
        // name: "dres",
        // pattern: "dres/{controller}");
        app.UseRouting();
        
        app.UseEndpoints(
            builder => builder.MapControllers());
    }
}

[ApiController]
[Route("dres/[controller]/[action]")]
public class ResourcesController : ControllerBase
{
    private readonly IResourcesProvider _resourcesProvider;
    private readonly IAssembliesResolver _assembliesResolver;

    public ResourcesController(
        IResourcesProvider resourcesProvider,
        IAssembliesResolver assembliesResolver)
    {
        _resourcesProvider = resourcesProvider;
        _assembliesResolver = assembliesResolver;
    }

    [HttpGet]
    public async Task<ActionResult<IImmutableList<Resource>>> All()
    {
        await Task.Yield();

        var assemblies = _assembliesResolver.GetAvailable();
        var resources = _resourcesProvider.Get(assemblies);
        return Ok(resources);
    }
}