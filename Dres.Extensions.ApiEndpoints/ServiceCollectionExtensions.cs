using Dres.Core;
using Dres.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Dres.Extensions.ApiEndpoints;

public static class ServiceCollectionExtensions
{
    public static void AddDresWithController(this IServiceCollection services, Action<DresOptions> options)
    {
        services.AddDres(options);
        services.AddControllers().AddApplicationPart(typeof(ResourcesController).Assembly);
    }
}