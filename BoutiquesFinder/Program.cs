using BoutiquesFinder.Application.Services.Implementation;
using BoutiquesFinder.Application.Services.Interface;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

//Dependency Injection
builder.Services.AddTransient<IBoutiqueFinderService, BoutiqueFinderService>();
builder.Services.AddTransient<IHttpService, HttpService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();

builder.Services.Configure<IISOptions>(options =>
{
    options.ForwardClientCertificate = false;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BOUTIQUESFINDER.API",
        Version = "v1",
        Description = @"<br>API exercise by João Veloso</br>"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.InjectJavascript("Geolocation.js");
    o.SwaggerEndpoint("/swagger/v1/swagger.json", "BOUTIQUESFINDER.API");
    o.RoutePrefix = string.Empty;
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();