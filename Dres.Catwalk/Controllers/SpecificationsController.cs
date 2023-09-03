using Dres.Catwalk.Controllers.ApiObjects;
using Dres.Catwalk.Extensions;
using Dres.Catwalk.Specifications.Core;
using Microsoft.AspNetCore.Mvc;

namespace Dres.Catwalk.Controllers;

[ApiController]
[Route("api/specifications")]
public class SpecificationsController : ControllerBase
{
    private readonly ILogger<SpecificationsController> _logger;
    private readonly ISpecificationsService _specificationsService;

    public SpecificationsController(
        ILogger<SpecificationsController> logger,
        ISpecificationsService specificationsService)
    {
        _logger = logger;
        _specificationsService = specificationsService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SpecificationAo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<SpecificationAo>>> All()
    {
        var specifications = await _specificationsService.GetAsync();
        var allSpecs = specifications
            .Select(s => s.ToAo());

        return Ok(allSpecs);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SpecificationAo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecificationAo>> Details([FromRoute] string id)
    {
        var specifications = await _specificationsService.GetAsync();

        var spec = specifications.FirstOrDefault(s => s.SpecificationId.Value == id);
        if (spec is null)
        {
            return NotFound();
        }

        return Ok(spec);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SpecificationAo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecificationAo>> Publish([FromBody] Dres.Core.Specification specificationData)
    {
        var now = DateTimeOffset.Now;
        var addedSpec = await _specificationsService.AddSpecificationAsync(specificationData, now);

        return Ok(addedSpec.ToAo());
    }
}