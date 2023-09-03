using Dres.Catwalk.Core.Module;
using Dres.Catwalk.Specifications.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dres.Catwalk.Specifications.FileSystem;

internal class SpecificationsFileSystemModule : IModule
{
    public string Name => "SpecificationsFileSystem";

    public void Register(WebApplicationBuilder builder)
    {
        var storageOptions = new SpecificationsStorageFileSystemOptions();
        var storageOptionsSection = builder.Configuration.GetSection(SpecificationsStorageFileSystemOptions.Position);
        storageOptionsSection.Bind(storageOptions);
        builder.Services.Configure<SpecificationsStorageFileSystemOptions>(storageOptionsSection);
        builder.Services.AddSingleton(storageOptions);

        SpecificationsDirectoryInitializer.EnsureCreated(storageOptions);

        builder.Services.AddScoped<ISpecificationsService, SpecificationsFromFileSystemService>();
    }

    public void Use(WebApplication app)
    {
    }
}