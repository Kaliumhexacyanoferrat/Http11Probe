namespace Http11Probe.TestCases;

public sealed class SequenceTestCase : ITestCase
{
    public required string Id { get; init; }
    public required string Description { get; init; }
    public required TestCategory Category { get; init; }
    public string? RfcReference { get; init; }
    public bool Scored { get; init; } = true;
    public RfcLevel RfcLevel { get; init; } = RfcLevel.Must;
    public required ExpectedBehavior Expected { get; init; }
    public required IReadOnlyList<SequenceStep> Steps { get; init; }
    public required Func<IReadOnlyList<StepResult>, TestVerdict> Validator { get; init; }
    public Func<IReadOnlyList<StepResult>, string?>? BehavioralAnalyzer { get; init; }
}
