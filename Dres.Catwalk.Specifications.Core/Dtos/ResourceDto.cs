using Dres.Core;

namespace Dres.Catwalk.Specifications.Core.Dtos;

public record ResourceDto(
    string Name,
    string DomainContext,
    string Identifier,
    string Type,
    ICollection<Property> Properties);