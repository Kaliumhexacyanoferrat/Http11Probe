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
