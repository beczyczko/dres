using Dres.Core;

namespace Dres.Catwalk.Specifications.Core.Dtos;

public record SpecificationDto(
    SpecificationId SpecificationId,
    string DresApiVersion,
    DateTimeOffset? CreatedOn,
    ICollection<ResourceDto> Resources);