using Http11Probe.Client;
using Http11Probe.Response;

namespace Http11Probe.TestCases;

public sealed class ExpectedBehavior
{
    public StatusCodeRange? ExpectedStatus { get; init; }
    public ConnectionState? ExpectedConnectionState { get; init; }
    public bool AllowConnectionClose { get; init; }
    public Func<HttpResponse?, ConnectionState, TestVerdict>? CustomValidator { get; init; }
    public string? Description { get; init; }

    public string GetDescription()
    {
        if (Description is not null)
            return Description;

        var parts = new List<string>();
        if (ExpectedStatus is not null)
            parts.Add(ExpectedStatus.ToString());
        if (AllowConnectionClose)
            parts.Add("close");
        return parts.Count > 0 ? string.Join(" or ", parts) : "custom";
    }

    public TestVerdict Evaluate(HttpResponse? response, ConnectionState connectionState)
    {
        if (CustomValidator is not null)
            return CustomValidator(response, connectionState);

        if (ExpectedConnectionState.HasValue && connectionState == ExpectedConnectionState.Value)
            return TestVerdict.Pass;

        if (AllowConnectionClose && connectionState == ConnectionState.ClosedByServer && response is null)
            return TestVerdict.Pass;

        if (response is null)
            return AllowConnectionClose ? TestVerdict.Pass : TestVerdict.Fail;

        if (ExpectedStatus is not null && ExpectedStatus.Contains(response.StatusCode))
            return TestVerdict.Pass;

        return TestVerdict.Fail;
    }
}
