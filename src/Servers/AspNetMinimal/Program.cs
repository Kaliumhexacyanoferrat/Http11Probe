var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:8080");

var app = builder.Build();

app.MapGet("/", () => "Hello from ASP.NET Minimal API");

app.Run();
