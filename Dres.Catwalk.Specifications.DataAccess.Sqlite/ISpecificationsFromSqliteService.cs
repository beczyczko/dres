using Dres.Catwalk.Specifications.Core;
using Dres.Catwalk.Specifications.Core.Dtos;
using Dres.Core;

namespace Dres.Catwalk.Specifications.DataAccess.Sqlite;

public interface ISpecificationsFromSqliteService : ISpecificationsService
{
    Task<SpecificationDto> AddSpecificationAsync(Specification specificationData, DateTimeOffset moment);
}