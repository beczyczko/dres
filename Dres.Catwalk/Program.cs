using System.Text.Json.Serialization;
using Dres.Catwalk.Extensions;
using Dres.Core;
using Dres.PlantumlServerIntegration;
using Dres.PlantumlServerIntegration.Settings;

var builder = WebApplication.CreateBuilder(args);

var plantumlServerOptions = new PlantumlServerOptions();
var plantumlServerConfigSection = builder.Configuration.GetSection(PlantumlServerOptions.Position);
plantumlServerConfigSection.Bind(plantumlServerOptions);
builder.Services.Configure<PlantumlServerOptions>(plantumlServerConfigSection);
builder.Services.AddHttpClient<IPlantumlServerClient, PlantumlServerClient>(client =>
{
    client.BaseAddress = new Uri(plantumlServerOptions.BaseUrl);
});

builder.AddSpecificationsStorage();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(document =>
{
    document.DocumentName = "web-api";
    document.Version = "1";
    document.Title = "Web API";
});

builder.Services.AddCors(options =>
    options.AddPolicy("default", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddScoped<IResourceRelationsPumlBuilder, ResourceRelationsPumlBuilder>();

builder.Services.AddHttpClient();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSpecificationsStorage();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("default");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi(document => document.DocumentName = "web-api");
    app.UseSwaggerUi3();
}

app.MapFallbackToFile("index.html");

app.Run();