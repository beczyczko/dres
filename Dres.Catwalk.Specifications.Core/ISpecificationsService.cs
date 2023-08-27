using Dres.Core;

namespace Dres.Catwalk.Specifications.Core;

public interface ISpecificationsService
{
    Task<List<Specification>> GetAsync();
}