using System.Collections.Immutable;
using System.Net.Mime;
using System.Text;
using Dres.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dres.Extensions.DependencyInjection;

[ApiController]
[Route("dres/resources")]
public class ResourcesController : ControllerBase
{
    private readonly IResourcesProvider _resourcesProvider;
    private readonly IAssembliesResolver _assembliesResolver;
    private readonly IResourceRelationsPumlBuilder _resourceRelationsPumlBuilder;

    public ResourcesController(
        IResourcesProvider resourcesProvider,
        IAssembliesResolver assembliesResolver,
        IResourceRelationsPumlBuilder resourceRelationsPumlBuilder)
    {
        _resourcesProvider = resourcesProvider;
        _assembliesResolver = assembliesResolver;
        _resourceRelationsPumlBuilder = resourceRelationsPumlBuilder;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IImmutableList<Resource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IImmutableList<Resource>>> All()
    {
        await Task.Yield();

        var assemblies = _assembliesResolver.GetAvailable();
        var resources = _resourcesProvider.Get(assemblies);
        return Ok(resources);
    }

    [HttpGet("as-puml-file")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<string>> AsPumlFile()
    {
        await Task.Yield();

        var assemblies = _assembliesResolver.GetAvailable();
        var resources = _resourcesProvider.Get(assemblies);

        var puml = _resourceRelationsPumlBuilder.Build(resources);

        return Ok(puml);
    }

    [HttpGet("download-puml-file")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DownloadPumlFile()
    {
        await Task.Yield();

        var assemblies = _assembliesResolver.GetAvailable();
        var resources = _resourcesProvider.Get(assemblies);

        var puml = _resourceRelationsPumlBuilder.Build(resources);

        var pumlBytes = Encoding.UTF8.GetBytes(puml);

        return File(pumlBytes, "text/plain", "dres.puml");
    }
}