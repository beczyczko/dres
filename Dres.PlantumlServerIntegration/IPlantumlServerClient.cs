namespace Dres.PlantumlServerIntegration;

public interface IPlantumlServerClient
{
    Task<EncodedPuml> Encode(string pumlContent);
    Task<HttpResponseMessage> Svg(EncodedPuml encodedPuml);
    Task<HttpResponseMessage> Svg(string pumlContent);
}