using System.Text.Json.Serialization;
using Dres.Catwalk.Database;
using Dres.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ResourcesDbContext>(options =>
    options.UseSqlite(connectionString));

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

builder.Services.AddTransient<IResourceRelationsPumlBuilder, ResourceRelationsPumlBuilder>();

builder.Services.AddHttpClient();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ResourcesDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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