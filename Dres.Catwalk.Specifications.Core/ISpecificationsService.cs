using Dres.Catwalk.Specifications.Core.Dtos;

namespace Dres.Catwalk.Specifications.Core;

public interface ISpecificationsService
{
    Task<List<SpecificationDto>> GetAsync();
}
