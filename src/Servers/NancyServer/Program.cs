using Nancy;
using Nancy.Hosting.Self;

var port = args.Length > 0 ? args[0] : "9006";
var uri = new Uri($"http://0.0.0.0:{port}");

var config = new HostConfiguration { RewriteLocalhost = false };

using var host = new NancyHost(config, uri);
host.Start();

Console.WriteLine($"Nancy listening on {uri}");

var waitHandle = new ManualResetEvent(false);
Console.CancelKeyPress += (_, e) => { e.Cancel = true; waitHandle.Set(); };
waitHandle.WaitOne();

public class EchoModule : NancyModule
{
    public EchoModule() : base("/echo")
    {
        Get("/", _ => EchoHeaders());
        Post("/", _ => EchoHeaders());
        Put("/", _ => EchoHeaders());
        Delete("/", _ => EchoHeaders());
        Patch("/", _ => EchoHeaders());
    }

    private string EchoHeaders()
    {
        var sb = new System.Text.StringBuilder();
        foreach (var h in Request.Headers)
            foreach (var v in h.Value)
                sb.AppendLine($"{h.Key}: {v}");
        return sb.ToString();
    }
}

public class HomeModule : NancyModule
{
    public HomeModule()
    {
        Get("/{path*}", _ => "OK");
        Get("/", _ => "OK");
        Post("/{path*}", _ => EchoBody());
        Post("/", _ => EchoBody());
    }

    private string EchoBody()
    {
        using var reader = new System.IO.StreamReader(Request.Body);
        return reader.ReadToEnd();
    }
}
