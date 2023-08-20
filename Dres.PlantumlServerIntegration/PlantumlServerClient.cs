using System.Net.Mime;
using System.Text;

namespace Dres.PlantumlServerIntegration;

internal class PlantumlServerClient : IPlantumlServerClient
{
    private readonly HttpClient _httpClient;

    public PlantumlServerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EncodedPuml> Encode(string pumlContent)
    {
        var stringContent = new StringContent(pumlContent, Encoding.UTF8, MediaTypeNames.Text.Plain);

        const string path = "plantuml/coder";
        using var httpResponseMessage = await _httpClient.PostAsync(
            path,
            stringContent
        );

        await using var contentStream =
            await httpResponseMessage.Content.ReadAsStreamAsync();
        var reader = new StreamReader(contentStream);

        var encodedPuml = await reader.ReadToEndAsync();

        return new EncodedPuml(encodedPuml);
    }

    public async Task<HttpResponseMessage> Svg(EncodedPuml encodedPuml)
    {
        var responseMessage = await _httpClient.GetAsync("plantuml/svg/" + encodedPuml.Content);
        return responseMessage;
    }

    public async Task<HttpResponseMessage> Svg(string pumlContent)
    {
        var encodedPuml = await Encode(pumlContent);
        var responseMessage = await Svg(encodedPuml);
        return responseMessage;
    }
}