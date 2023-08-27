using System.Collections.Immutable;
using System.Net.Mime;
using System.Text;
using Dres.Catwalk.Database;
using Dres.Catwalk.Extensions;
using Dres.Catwalk.Specifications.FileSystem;
using Dres.Core;
using Dres.PlantumlServerIntegration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dres.Catwalk.Controllers;

[ApiController]
[Route("api/puml")]
public class PumlController : ControllerBase
{
    private readonly ILogger<PumlController> _logger;
    private readonly IResourceRelationsPumlBuilder _resourceRelationsPumlBuilder;
    private readonly ResourcesDbContext _resourcesDbContext;
    private readonly IPlantumlServerClient _plantumlServerClient;
    private readonly ISpecificationsFromFileSystemService _specificationsFromFileSystemService;

    public PumlController(
        ILogger<PumlController> logger,
        IResourceRelationsPumlBuilder resourceRelationsPumlBuilder,
        ResourcesDbContext resourcesDbContext,
        IPlantumlServerClient plantumlServerClient,
        ISpecificationsFromFileSystemService specificationsFromFileSystemService)
    {
        _logger = logger;
        _resourceRelationsPumlBuilder = resourceRelationsPumlBuilder;
        _resourcesDbContext = resourcesDbContext;
        _plantumlServerClient = plantumlServerClient;
        _specificationsFromFileSystemService = specificationsFromFileSystemService;
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
        //tododb create some serice that will retrieve specs and resources from various places
        var resources = await _resourcesDbContext.Specifications
            .Include(s => s.Resources)
            .ThenInclude(r => r.Properties)
            .Where(s => specIds.Contains(s.SpecificationId.Value))
            .SelectMany(s => s.Resources)
            .ToListAsync();

        var fileSpecifications = await _specificationsFromFileSystemService.GetAsync();
        var fileSpecResources = fileSpecifications
            .Where(s => specIds.Contains(s.SpecificationId.Value))
            .SelectMany(s => s.Resources)
            .Select(r => r.ToDresCoreResource());
        
        return resources
            .Select(r => r.ToDresCoreResource())
            .Concat(fileSpecResources)
            .ToImmutableList();
    }
}