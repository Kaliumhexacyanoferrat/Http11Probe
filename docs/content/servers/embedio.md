---
title: "EmbedIO"
toc: false
breadcrumbs: false
---

**Language:** C# · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/EmbedIOServer)

## Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props .
COPY src/Servers/EmbedIOServer/ src/Servers/EmbedIOServer/
RUN dotnet restore src/Servers/EmbedIOServer/EmbedIOServer.csproj
RUN dotnet publish src/Servers/EmbedIOServer/EmbedIOServer.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EmbedIOServer.dll", "8080"]
```

## Source — `Program.cs`

```csharp
using EmbedIO;
using EmbedIO.Actions;

var port = args.Length > 0 && int.TryParse(args[0], out var p) ? p : 8080;
var url = $"http://*:{port}/";

using var server = new WebServer(o => o
    .WithUrlPrefix(url)
    .WithMode(HttpListenerMode.EmbedIO))
    .WithModule(new ActionModule("/", HttpVerbs.Any, async ctx =>
    {
        ctx.Response.ContentType = "text/plain";
        if (ctx.Request.HttpVerb == HttpVerbs.Post)
        {
            using var reader = new System.IO.StreamReader(ctx.Request.InputStream);
            var body = await reader.ReadToEndAsync();
            await ctx.SendStringAsync(body, "text/plain", System.Text.Encoding.UTF8);
        }
        else
        {
            await ctx.SendStringAsync("OK", "text/plain", System.Text.Encoding.UTF8);
        }
    }));

Console.WriteLine($"EmbedIO listening on http://localhost:{port}");
await server.RunAsync();
```
