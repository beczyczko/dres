using Microsoft.AspNetCore.Builder;

namespace Dres.Extensions.ApplicationBuilder;

public static class ApplicationBuilderExtensions
{
    public static void UseDres(this IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseEndpoints(builder => builder.MapControllerRoute(
            name: "dres",
            pattern: "dres/{controller}"));
    }
}
