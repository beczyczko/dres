using System.Text.Json;
using Dres.Catwalk.Specifications.Core;
using Dres.Catwalk.Specifications.Core.Dtos;
using Dres.Catwalk.Specifications.FileSystem.Extensions;
using Dres.Core;

namespace Dres.Catwalk.Specifications.FileSystem;

internal class SpecificationsFromFileSystemService : ISpecificationsService
{
    private readonly SpecificationsStorageFileSystemOptions _options;

    public SpecificationsFromFileSystemService(SpecificationsStorageFileSystemOptions options)
    {
        _options = options;
    }

    public async Task<List<SpecificationDto>> GetAsync()
    {
        List<Specification> specifications = new();

        var jsonFiles = Directory.GetFiles(_options.BasePath, "*.json");

        foreach (var jsonFile in jsonFiles)
        {
            await using var fileStream = File.OpenRead(jsonFile);
            var resource = await JsonSerializer.DeserializeAsync<Specification>(fileStream,
                options: new JsonSerializerOptions
                {
                    IncludeFields = true,
                    PropertyNameCaseInsensitive = true
                });
            if (resource != null)
            {
                specifications.Add(resource);
            }
        }

        return specifications.ConvertAll(s => s.ToDto());
    }

    public async Task<SpecificationDto> AddSpecificationAsync(Specification specificationData, DateTimeOffset moment)
    {
        var baseFileName = specificationData.SpecificationId.Value;
        var invalidChars = Path.GetInvalidFileNameChars();
        foreach (var invalidChar in invalidChars)
        {
            baseFileName = baseFileName.Replace(invalidChar, '_');
        }

        const string fileExtension = ".json";

        var basePath = PrepareBasePath();

        var path = $"{basePath}/{baseFileName}{fileExtension}";

        await using var fileStream = File.OpenWrite(path);

        var options = new JsonSerializerOptions { WriteIndented = true };
        await JsonSerializer.SerializeAsync(fileStream, specificationData, options);

        await fileStream.DisposeAsync();

        return specificationData.ToDto();
    }

    private string PrepareBasePath()
    {
        var preparedBasePath = _options.BasePath;
        if (preparedBasePath.EndsWith("/"))
        {
            preparedBasePath = preparedBasePath[..^1];
        }

        return preparedBasePath;
    }
}