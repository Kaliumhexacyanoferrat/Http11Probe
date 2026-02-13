---
title: "SimpleW"
toc: false
breadcrumbs: false
---

**Language:** C# · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/SimpleWServer)

## Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props .
COPY src/Servers/SimpleWServer/ src/Servers/SimpleWServer/
RUN dotnet restore src/Servers/SimpleWServer/SimpleWServer.csproj
RUN dotnet publish src/Servers/SimpleWServer/SimpleWServer.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SimpleWServer.dll", "8080"]
```

## Source — `Program.cs`

```csharp
using System.Net;
using SimpleW;

var port = args.Length > 0 && int.TryParse(args[0], out var p) ? p : 8080;

var server = new SimpleWServer(IPAddress.Any, port);


server.MapGet("/echo", (HttpSession session) =>
{
    var sb = new System.Text.StringBuilder();
    foreach (var h in session.Request.Headers.EnumerateAll())
        sb.AppendLine($"{h.Key}: {h.Value}");
    return sb.ToString();
});
server.MapPost("/echo", (HttpSession session) =>
{
    var sb = new System.Text.StringBuilder();
    foreach (var h in session.Request.Headers.EnumerateAll())
        sb.AppendLine($"{h.Key}: {h.Value}");
    return sb.ToString();
});
server.MapGet("/", () => "OK");
server.MapGet("/{path}", () => "OK");
server.MapPost("/", (HttpSession session) => session.Request.BodyString);
server.MapPost("/{path}", (HttpSession session) => session.Request.BodyString);

Console.WriteLine($"SimpleW listening on http://localhost:{port}");

await server.RunAsync();
```
