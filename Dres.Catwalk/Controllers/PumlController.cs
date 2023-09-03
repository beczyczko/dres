using System.Collections.Immutable;
using System.Net.Mime;
using System.Text;
using Dres.Catwalk.Extensions;
using Dres.Catwalk.Specifications.Core;
using Dres.Core;
using Dres.PlantumlServerIntegration;
using Microsoft.AspNetCore.Mvc;

namespace Dres.Catwalk.Controllers;

[ApiController]
[Route("api/puml")]
public class PumlController : ControllerBase
{
    private readonly ILogger<PumlController> _logger;
    private readonly IResourceRelationsPumlBuilder _resourceRelationsPumlBuilder;
    private readonly IPlantumlServerClient _plantumlServerClient;
    private readonly ISpecificationsService _specificationsService;

    public PumlController(
        ILogger<PumlController> logger,
        IResourceRelationsPumlBuilder resourceRelationsPumlBuilder,
        IPlantumlServerClient plantumlServerClient,
        ISpecificationsService specificationsService)
    {
        _logger = logger;
        _resourceRelationsPumlBuilder = resourceRelationsPumlBuilder;
        _plantumlServerClient = plantumlServerClient;
        _specificationsService = specificationsService;
    }

    [HttpGet("combine")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<string>> AsPumlFile([FromQuery] string[] specIds)
    {
        await Task.Yield();

        var resources = await ResourcesBySpecIds(specIds);
        var puml = _resourceRelationsPumlBuilder.Build(resources);

        return Ok(puml);
    }

    [HttpGet("combine/download-puml-file")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DownloadPumlFile([FromQuery] string[] specIds)
    {
        await Task.Yield();

        var resources = await ResourcesBySpecIds(specIds);
        var puml = _resourceRelationsPumlBuilder.Build(resources);

        var pumlBytes = Encoding.UTF8.GetBytes(puml);

        return File(pumlBytes, "text/plain", "dres.puml");
    }

    [HttpGet("combine/svg")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Image.Svg)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Svg([FromQuery] string[] specIds)
    {
        var resources = await ResourcesBySpecIds(specIds);
        var puml = _resourceRelationsPumlBuilder.Build(resources);
        var responseMessage = await _plantumlServerClient.Svg(puml);

        return await responseMessage.ToContentResultAsync("image/svg+xml");
    }

    private async Task<IImmutableList<Resource>> ResourcesBySpecIds(string[] specIds)
    {
        var specifications = await _specificationsService.GetAsync();

        var specResources = specifications
            .Where(s => specIds.Contains(s.SpecificationId.Value))
            .SelectMany(s => s.Resources)
            .Select(r => r.ToDresCoreResource());
        
        return specResources.ToImmutableList();
    }
}