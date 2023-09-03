using Dres.Catwalk.Specifications.Core.Dtos;
using Dres.Core;

namespace Dres.Catwalk.Specifications.Core;

public interface ISpecificationsService
{
    Task<List<SpecificationDto>> GetAsync();
    Task<SpecificationDto> AddSpecificationAsync(Specification specificationData, DateTimeOffset moment);
}