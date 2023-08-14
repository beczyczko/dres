using Microsoft.AspNetCore.Mvc;

namespace Dres.Catwalk.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<ContentResult> ToContentResultAsync(
        this HttpResponseMessage responseMessage, string contentType)
    {
        var contentRes = new ContentResult
        {
            StatusCode = (int)responseMessage.StatusCode,
            Content = await responseMessage.Content.ReadAsStringAsync(),
            ContentType = contentType
        };
        return contentRes;
    }
}