---
title: "Sisk"
toc: false
breadcrumbs: false
---

**Language:** C# · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/SiskServer)

## Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props .
COPY src/Servers/SiskServer/ src/Servers/SiskServer/
RUN dotnet restore src/Servers/SiskServer/SiskServer.csproj
RUN dotnet publish src/Servers/SiskServer/SiskServer.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SiskServer.dll", "8080"]
```

## Source — `Program.cs`

```csharp
using Sisk.Core.Http;
using Sisk.Core.Routing;

var port = args.Length > 0 && int.TryParse(args[0], out var p) ? p : 8080;

using var app = HttpServer.CreateBuilder()
    .UseListeningPort($"http://+:{port}/")
    .Build();

app.Router.SetRoute(RouteMethod.Any, Route.AnyPath, request =>
{
    if (request.Method == HttpMethod.Post && request.Body is not null)
    {
        var body = request.Body;
        return new HttpResponse(200).WithContent(body);
    }
    return new HttpResponse(200).WithContent("OK");
});

await app.StartAsync();
```
