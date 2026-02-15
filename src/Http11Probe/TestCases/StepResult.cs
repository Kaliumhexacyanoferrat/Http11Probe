using Http11Probe.Client;
using Http11Probe.Response;

namespace Http11Probe.TestCases;

public sealed class StepResult
{
    public required string Label { get; init; }
    public bool Executed { get; init; }
    public HttpResponse? Response { get; init; }
    public ConnectionState ConnectionState { get; init; }
    public string? RawRequest { get; init; }
}
