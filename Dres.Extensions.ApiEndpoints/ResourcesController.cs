using System.Net.Mime;
using System.Text;
using Dres.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dres.Extensions.ApiEndpoints;

[ApiController]
[Route("dres/resources")]
public class ResourcesController : ControllerBase
{
    private readonly IResourcesProvider _resourcesProvider;
    private readonly IAssembliesResolver _assembliesResolver;
    private readonly IResourceRelationsPumlBuilder _resourceRelationsPumlBuilder;
    private readonly ISpecificationProvider _specificationProvider;

    public ResourcesController(
        IResourcesProvider resourcesProvider,
        IAssembliesResolver assembliesResolver,
        IResourceRelationsPumlBuilder resourceRelationsPumlBuilder,
        ISpecificationProvider specificationProvider)
    {
        _resourcesProvider = resourcesProvider;
        _assembliesResolver = assembliesResolver;
        _resourceRelationsPumlBuilder = resourceRelationsPumlBuilder;
        _specificationProvider = specificationProvider;
    }

    [HttpGet("full-spec")]
    [ProducesResponseType(typeof(Specification), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Specification>> FullSpecification()
    {
        await Task.Yield();

        return Ok(_specificationProvider.Get());
    }

    [HttpGet("full-spec/puml-file")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<string>> PumlFile()
    {
        await Task.Yield();

        var assemblies = _assembliesResolver.GetAvailable();
        var resources = _resourcesProvider.Get(assemblies);

        var puml = _resourceRelationsPumlBuilder.Build(resources);

        return Ok(puml);
    }

    [HttpGet("full-spec/download-puml-file")]
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