using Dres.Catwalk.Specifications.Core.Dtos;
using Dres.Catwalk.Specifications.Core.Extensions;
using Dres.Core;
using Microsoft.EntityFrameworkCore;

namespace Dres.Catwalk.Specifications.DataAccess.Sqlite;

public class SpecificationsFromSqliteService : ISpecificationsFromSqliteService
{
    private readonly ResourcesDbContext _context;

    public SpecificationsFromSqliteService(ResourcesDbContext context)
    {
        _context = context;
    }

    public async Task<List<SpecificationDto>> GetAsync()
    {
        var specifications = await _context.Specifications
            .Include(s => s.Resources)
            .ThenInclude(r => r.Properties)
            .ToListAsync();
        return specifications.ConvertAll(s => s.ToDto());
    }

    public async Task<SpecificationDto> AddSpecificationAsync(Specification specificationData, DateTimeOffset moment)
    {
        var specification = new Dres.Catwalk.Domain.Specification(specificationData, moment);

        _context.Specifications.Add(specification);
        await _context.SaveChangesAsync();

        return specification.ToDto();
    }
}