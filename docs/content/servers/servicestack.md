---
title: "ServiceStack"
toc: false
breadcrumbs: false
---

**Language:** C# · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/ServiceStackServer)

## Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props .
COPY src/Servers/ServiceStackServer/ src/Servers/ServiceStackServer/
RUN dotnet restore src/Servers/ServiceStackServer/ServiceStackServer.csproj
RUN dotnet publish src/Servers/ServiceStackServer/ServiceStackServer.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ServiceStackServer.dll"]
```

## Source — `Program.cs`

```csharp
using ServiceStack;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseServiceStack(new AppHost());
app.Map("/echo", (HttpContext ctx) =>
{
    var sb = new System.Text.StringBuilder();
    foreach (var h in ctx.Request.Headers)
        foreach (var v in h.Value)
            sb.AppendLine($"{h.Key}: {v}");
    return Results.Text(sb.ToString());
});
app.MapFallback(async (HttpContext ctx) =>
{
    if (ctx.Request.Method == "POST")
    {
        using var reader = new StreamReader(ctx.Request.Body);
        var body = await reader.ReadToEndAsync();
        return Results.Text(body);
    }
    return Results.Ok("OK");
});
app.Run("http://0.0.0.0:8080");

class AppHost : AppHostBase
{
    public AppHost() : base("Probe", typeof(AppHost).Assembly) { }
    public override void Configure() { }
}
```
