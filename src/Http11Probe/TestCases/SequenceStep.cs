namespace Http11Probe.TestCases;

public sealed class SequenceStep
{
    public required Func<TestContext, byte[]> PayloadFactory { get; init; }
    public string? Label { get; init; }
}
