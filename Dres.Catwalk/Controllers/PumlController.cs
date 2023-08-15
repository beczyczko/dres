using System.Collections.Immutable;
using System.Net.Mime;
using System.Text;
using Dres.Catwalk.Database;
using Dres.Catwalk.Extensions;
using Dres.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dres.Catwalk.Controllers;

[ApiController]
[Route("api/puml")]
public class PumlController : ControllerBase
{
    private readonly ILogger<PumlController> _logger;
    private readonly IResourceRelationsPumlBuilder _resourceRelationsPumlBuilder;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ResourcesDbContext _resourcesDbContext;

    public PumlController(
        ILogger<PumlController> logger,
        IResourceRelationsPumlBuilder resourceRelationsPumlBuilder,
        IHttpClientFactory httpClientFactory,
        ResourcesDbContext resourcesDbContext)
    {
        _logger = logger;
        _resourceRelationsPumlBuilder = resourceRelationsPumlBuilder;
        _httpClientFactory = httpClientFactory;
        _resourcesDbContext = resourcesDbContext;
    }

    [HttpGet("combine")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<string>> AsPumlFile([FromQuery] int[] specIds)
    {
        await Task.Yield();

        var resources = await ResourcesBySpecIds(specIds);
        var puml = _resourceRelationsPumlBuilder.Build(resources);

        return Ok(puml);
    }

    [HttpGet("combine/download-puml-file")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DownloadPumlFile([FromQuery] int[] specIds)
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
    public async Task<IActionResult> Svg([FromQuery] int[] specIds)
    {
        //todo db cleanup
        var resources = await ResourcesBySpecIds(specIds);
        var puml = _resourceRelationsPumlBuilder.Build(resources);

        var stringContent = new StringContent(puml, Encoding.UTF8, MediaTypeNames.Text.Plain);

        var httpClient = _httpClientFactory.CreateClient();
        using var httpResponseMessage = await httpClient.PostAsync(
            "http://localhost:7091/plantuml/coder",
            stringContent
        );

        await using var contentStream =
            await httpResponseMessage.Content.ReadAsStreamAsync();
        var reader = new StreamReader(contentStream);

        var encodedPuml = await reader.ReadToEndAsync();

        var responseMessage = await httpClient.GetAsync("http://localhost:7091/plantuml/svg/" + encodedPuml);
        return await responseMessage.ToContentResultAsync("image/svg+xml");
    }

    private async Task<IImmutableList<Resource>> ResourcesBySpecIds(int[] specIds)
    {
        var resources = await _resourcesDbContext.Specifications
            .Include(s => s.Resources)
            .ThenInclude(r => r.Properties)
            .Where(s => specIds.Contains(s.Id))
            .SelectMany(s => s.Resources)
            .ToListAsync();

        return resources
            .Select(r => r.ToDresCoreResource())
            .ToImmutableList();
    }
}