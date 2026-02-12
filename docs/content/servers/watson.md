---
title: "Watson"
toc: false
breadcrumbs: false
---

**Language:** C# · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/WatsonServer)

## Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props .
COPY src/Servers/WatsonServer/ src/Servers/WatsonServer/
RUN dotnet restore src/Servers/WatsonServer/WatsonServer.csproj
RUN dotnet publish src/Servers/WatsonServer/WatsonServer.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WatsonServer.dll", "8080"]
```

## Source — `Program.cs`

```csharp
using WatsonWebserver;
using WatsonWebserver.Core;

var port = args.Length > 0 && int.TryParse(args[0], out var p) ? p : 8080;

var settings = new WebserverSettings("*", port);
var server = new Webserver(settings, async ctx =>
{
    ctx.Response.StatusCode = 200;
    ctx.Response.ContentType = "text/plain";
    if (ctx.Request.Method == WatsonWebserver.Core.HttpMethod.POST && ctx.Request.Data != null)
    {
        using var reader = new StreamReader(ctx.Request.Data);
        var body = await reader.ReadToEndAsync();
        await ctx.Response.Send(body);
    }
    else
    {
        await ctx.Response.Send("OK");
    }
});

server.Start();

Console.WriteLine($"Watson listening on http://localhost:{port}");

var waitHandle = new ManualResetEvent(false);
Console.CancelKeyPress += (_, e) => { e.Cancel = true; waitHandle.Set(); };
waitHandle.WaitOne();

server.Stop();
```
