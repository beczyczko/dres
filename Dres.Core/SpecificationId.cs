using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dres.Core;

public class SpecificationId
{
    [Required] public string Name { get; private set; }
    [Required] public string Tag { get; private set; }
    [Required] public string Value { get; private set; }

    private SpecificationId()
    {
        // EF needs it to generate migrations        
    }

    public SpecificationId(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Value could not be null or empty", nameof(value));
        }

        var splitToNameAndTag = value.Split(':');

        Name = splitToNameAndTag[0];
        Tag = splitToNameAndTag.Length > 1 ? splitToNameAndTag[1] : string.Empty;
        Value = $"{Name}:{Tag}";
    }

    [JsonConstructor]
    public SpecificationId(string name, string tag)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value could not be null or empty", nameof(name));
        }

        if (string.IsNullOrEmpty(tag))
        {
            throw new ArgumentException("Value could not be null or empty", nameof(tag));
        }

        Name = name;
        Tag = tag;
        Value = $"{Name}:{Tag}";
    }
}