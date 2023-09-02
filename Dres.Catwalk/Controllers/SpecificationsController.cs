using Dres.Catwalk.Controllers.ApiObjects;
using Dres.Catwalk.Extensions;
using Dres.Catwalk.Specifications.DataAccess.Sqlite;
using Dres.Catwalk.Specifications.FileSystem;
using Microsoft.AspNetCore.Mvc;

namespace Dres.Catwalk.Controllers;

[ApiController]
[Route("api/specifications")]
public class SpecificationsController : ControllerBase
{
    private readonly ILogger<SpecificationsController> _logger;
    private readonly ISpecificationsFromFileSystemService _specificationsFromFileSystemService;
    private readonly ISpecificationsFromSqliteService _specificationsFromSqliteService;

    public SpecificationsController(
        ILogger<SpecificationsController> logger,
        ISpecificationsFromFileSystemService specificationsFromFileSystemService,
        ISpecificationsFromSqliteService specificationsFromSqliteService)
    {
        _logger = logger;
        _specificationsFromFileSystemService = specificationsFromFileSystemService;
        _specificationsFromSqliteService = specificationsFromSqliteService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SpecificationAo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<SpecificationAo>>> All()
    {
        var specifications = await _specificationsFromSqliteService.GetAsync();
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
        var specifications = await _specificationsFromSqliteService.GetAsync();
        var fileSpecifications = await _specificationsFromFileSystemService.GetAsync();
        var allSpecs = specifications.Concat(fileSpecifications);

        var spec = allSpecs.FirstOrDefault(s => s.SpecificationId.Value == id);
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
        var addedSpec = await _specificationsFromSqliteService.AddSpecificationAsync(specificationData, now);

        return Ok(addedSpec.ToAo());
    }
}