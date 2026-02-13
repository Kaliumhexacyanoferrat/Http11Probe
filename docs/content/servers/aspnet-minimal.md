---
title: "ASP.NET Minimal"
toc: false
breadcrumbs: false
---

**Language:** C# · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/AspNetMinimal)

## Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props .
COPY src/Servers/AspNetMinimal/ src/Servers/AspNetMinimal/
RUN dotnet restore src/Servers/AspNetMinimal/AspNetMinimal.csproj
RUN dotnet publish src/Servers/AspNetMinimal/AspNetMinimal.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AspNetMinimal.dll"]
```

## Source — `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://+:8080");

var app = builder.Build();

app.MapGet("/", () => "OK");

app.MapPost("/", async (HttpContext ctx) =>
{
    using var reader = new StreamReader(ctx.Request.Body);
    var body = await reader.ReadToEndAsync();
    return Results.Text(body);
});

app.Map("/echo", (HttpContext ctx) =>
{
    var sb = new System.Text.StringBuilder();
    foreach (var h in ctx.Request.Headers)
        foreach (var v in h.Value)
            sb.AppendLine($"{h.Key}: {v}");
    return Results.Text(sb.ToString());
});

app.Run();
```
