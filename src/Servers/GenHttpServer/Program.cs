using GenHTTP.Api.Content;
using GenHTTP.Api.Protocol;
using GenHTTP.Engine.Internal;
using GenHTTP.Modules.Functional;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Practices;

var port = args.Length > 0 && int.TryParse(args[0], out var p) ? p : 8080;

var echoHandler = Inline.Create()
    .Any(async (IRequest request) =>
    {
        await ValueTask.CompletedTask;
        var sb = new System.Text.StringBuilder();
        foreach (var h in request.Headers)
            sb.AppendLine($"{h.Key}: {h.Value}");
        return sb.ToString();
    });

var rootHandler = Inline.Create()
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

var layout = Layout.Create()
    .Add("echo", echoHandler)
    .Add(rootHandler);

await Host.Create()
    .Handler(layout)
    .Defaults()
    .Port((ushort)port)
    .RunAsync();
