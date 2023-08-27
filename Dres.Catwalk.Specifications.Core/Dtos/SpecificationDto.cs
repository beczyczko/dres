using Dres.Core;

namespace Dres.Catwalk.Specifications.Core.Dtos;

//tododb useit
public record SpecificationDto(
    string Name,
    string Tag,
    string DresApiVersion,
    DateTimeOffset? CreatedOn,
    ICollection<Resource> Resources);