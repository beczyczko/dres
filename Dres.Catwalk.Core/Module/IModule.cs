using Microsoft.AspNetCore.Builder;

namespace Dres.Catwalk.Core.Module;

public interface IModule
{
    string Name { get; }
    void Register(WebApplicationBuilder builder);
    void Use(WebApplication app);
}