---
title: "GenHTTP"
toc: false
breadcrumbs: false
---

**Language:** C# · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/GenHttpServer)

## Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props .
COPY src/Servers/GenHttpServer/ src/Servers/GenHttpServer/
RUN dotnet restore src/Servers/GenHttpServer/GenHttpServer.csproj
RUN dotnet publish src/Servers/GenHttpServer/GenHttpServer.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "GenHttpServer.dll", "8080"]
```

## Source — `Program.cs`

```csharp
using GenHTTP.Api.Content;
using GenHTTP.Api.Protocol;
using GenHTTP.Engine.Internal;
using GenHTTP.Modules.Functional;
using GenHTTP.Modules.Practices;

var port = args.Length > 0 && int.TryParse(args[0], out var p) ? p : 8080;

var handler = Inline.Create()
    .Get(async (IRequest request) =>
    {
        await ValueTask.CompletedTask;
        return "OK";
    })
    .Post(async (IRequest request) =>
    {
        if (request.Content is not null)
        {
            using var reader = new StreamReader(request.Content);
            return await reader.ReadToEndAsync();
        }
        return "";
    });

await Host.Create()
    .Handler(handler)
    .Defaults()
    .Port((ushort)port)
    .RunAsync();
```
