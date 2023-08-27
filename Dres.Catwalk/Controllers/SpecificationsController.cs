using Dres.Catwalk.Controllers.ApiObjects;
using Dres.Catwalk.Database;
using Dres.Catwalk.Domain;
using Dres.Catwalk.Extensions;
using Dres.Catwalk.Specifications.FileSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dres.Catwalk.Controllers;

[ApiController]
[Route("api/specifications")]
public class SpecificationsController : ControllerBase
{
    private readonly ILogger<SpecificationsController> _logger;
    private readonly ResourcesDbContext _resourcesDbContext;
    private readonly ISpecificationsFromFileSystemService _specificationsFromFileSystemService;

    public SpecificationsController(
        ILogger<SpecificationsController> logger,
        ResourcesDbContext resourcesDbContext,
        ISpecificationsFromFileSystemService specificationsFromFileSystemService)
    {
        _logger = logger;
        _resourcesDbContext = resourcesDbContext;
        _specificationsFromFileSystemService = specificationsFromFileSystemService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SpecificationAo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<SpecificationAo>>> All()
    {
        var specifications = await _resourcesDbContext.Specifications
            .Include(s => s.Resources)
            .ThenInclude(r => r.Properties)
            .ToListAsync();

        var fileSpecifications = await _specificationsFromFileSystemService.GetAsync();

        var allSpecs = specifications
            .Select(s => s.ToAo())
            .Concat(fileSpecifications.ConvertAll(s => s.ToAo()));

        return Ok(allSpecs);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SpecificationAo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecificationAo>> Details([FromRoute] string id)
    {
        var spec = await _resourcesDbContext.Specifications
            .Include(s => s.Resources)
            .ThenInclude(r => r.Properties)
            .SingleOrDefaultAsync(s => s.SpecificationId.Value == id);
        if (spec is null)
        {
            return NotFound();
        }

        return Ok(spec);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SpecificationAo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecificationAo>> Publish([FromBody] Core.Specification specificationData)
    {
        var now = DateTimeOffset.Now;

        var specification = new Specification(specificationData, now);

        _resourcesDbContext.Specifications.Add(specification);
        await _resourcesDbContext.SaveChangesAsync();

        return Ok(specification.ToAo());
    }
}