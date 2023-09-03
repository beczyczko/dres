using Dres.Catwalk.Specifications.Core;
using Dres.Catwalk.Specifications.DataAccess.Sqlite;
using Dres.Catwalk.Specifications.FileSystem;

namespace Dres.Catwalk.Extensions;

internal static class WebApplicationExtensions
{
    private const string SpecificationsStorageTypeNotProvidedError = "Specifications storage type has to be provided";
    private static readonly SpecificationsStorageOptions StorageOptions = new();

    public static WebApplication UseSpecificationsStorage(this WebApplication app)
    {
        switch (StorageOptions.Type)
        {
            case SpecificationsStorage.FileSystem:
                var specificationsFileSystemModule = new SpecificationsFileSystemModule();
                specificationsFileSystemModule.Use(app);
                break;
            case SpecificationsStorage.SqLite:
                var specificationsSqLiteModule = new SpecificationsSqLiteModule();
                specificationsSqLiteModule.Use(app);
                break;
            default:
                throw new ArgumentOutOfRangeException(
                    SpecificationsStorageTypeNotProvidedError,
                    nameof(StorageOptions.Type));
        }

        return app;
    }

    public static WebApplicationBuilder AddSpecificationsStorage(this WebApplicationBuilder builder)
    {
        var storageOptionsSection =
            builder.Configuration.GetSection(SpecificationsStorageOptions.Position);
        storageOptionsSection.Bind(StorageOptions);
        builder.Services.Configure<SpecificationsStorageOptions>(storageOptionsSection);

        switch (StorageOptions.Type)
        {
            case SpecificationsStorage.FileSystem:
                var specificationsFileSystemModule = new SpecificationsFileSystemModule();
                specificationsFileSystemModule.Register(builder);
                break;
            case SpecificationsStorage.SqLite:
                var specificationsSqLiteModule = new SpecificationsSqLiteModule();
                specificationsSqLiteModule.Register(builder);
                break;
            default:
                throw new ArgumentOutOfRangeException(
                    SpecificationsStorageTypeNotProvidedError,
                    nameof(StorageOptions.Type));
        }

        return builder;
    }
}