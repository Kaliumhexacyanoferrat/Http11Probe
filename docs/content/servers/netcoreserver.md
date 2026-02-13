---
title: "NetCoreServer"
toc: false
breadcrumbs: false
---

**Language:** C# · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/NetCoreServerFramework)

## Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props .
COPY src/Servers/NetCoreServerFramework/ src/Servers/NetCoreServerFramework/
RUN dotnet restore src/Servers/NetCoreServerFramework/NetCoreServerFramework.csproj
RUN dotnet publish src/Servers/NetCoreServerFramework/NetCoreServerFramework.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "NetCoreServerFramework.dll", "8080"]
```

## Source — `Program.cs`

```csharp
using System.Net;
using System.Net.Sockets;
using NetCoreServer;

var port = args.Length > 0 && int.TryParse(args[0], out var p) ? p : 8080;

var server = new OkHttpServer(IPAddress.Any, port);
server.Start();

Console.WriteLine($"NetCoreServer listening on http://localhost:{port}");

var waitHandle = new ManualResetEvent(false);
Console.CancelKeyPress += (_, e) => { e.Cancel = true; waitHandle.Set(); };
waitHandle.WaitOne();

server.Stop();

class OkHttpSession : HttpSession
{
    public OkHttpSession(NetCoreServer.HttpServer server) : base(server) { }

    protected override void OnReceivedRequest(HttpRequest request)
    {
        if (request.Url == "/echo")
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < request.Headers; i++)
            {
                var (name, value) = request.Header(i);
                sb.AppendLine($"{name}: {value}");
            }
            SendResponseAsync(Response.MakeOkResponse(200).SetBody(sb.ToString()));
        }
        else if (request.Method == "POST" && request.Body.Length > 0)
            SendResponseAsync(Response.MakeOkResponse(200).SetBody(request.Body));
        else
            SendResponseAsync(Response.MakeOkResponse(200).SetBody("OK"));
    }

    protected override void OnReceivedRequestError(HttpRequest request, string error)
    {
        SendResponseAsync(Response.MakeErrorResponse(400));
    }

    protected override void OnError(SocketError error) { }
}

class OkHttpServer : NetCoreServer.HttpServer
{
    public OkHttpServer(IPAddress address, int port) : base(address, port) { }

    protected override TcpSession CreateSession() => new OkHttpSession(this);

    protected override void OnError(SocketError error) { }
}
```
