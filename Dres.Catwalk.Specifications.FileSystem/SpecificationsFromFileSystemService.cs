using System.Text.Json;
using Dres.Core;

namespace Dres.Catwalk.Specifications.FileSystem;

public class SpecificationsFromFileSystemService : ISpecificationsFromFileSystemService
{
    public async Task<List<Specification>> GetAsync()
    {
        List<Specification> specifications = new();

        var jsonFiles = Directory.GetFiles(SpecificationsDirectory.Path, "*.json");

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

        return specifications;
    }
}