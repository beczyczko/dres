using Dres.Catwalk.Core.Module;
using Dres.Catwalk.Specifications.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dres.Catwalk.Specifications.DataAccess.Sqlite;

internal class SpecificationsSqLiteModule : IModule
{
    public string Name => "SpecificationsSqLite";

    public void Register(WebApplicationBuilder builder)
    {
        var storageOptions = new SpecificationsStorageSqLiteOptions();
        var storageOptionsSection = builder.Configuration.GetSection(SpecificationsStorageSqLiteOptions.Position);
        storageOptionsSection.Bind(storageOptions);
        builder.Services.Configure<SpecificationsStorageSqLiteOptions>(storageOptionsSection);
        var connectionString = storageOptions.ConnectionString;

        builder.Services.AddDbContext<ResourcesDbContext>(options =>
            options.UseSqlite(connectionString));
        builder.Services.AddScoped<ISpecificationsService, SpecificationsFromSqliteService>();
    }

    public void Use(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ResourcesDbContext>();
        db.Database.Migrate();
    }
}