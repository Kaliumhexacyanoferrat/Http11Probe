namespace Http11Probe.TestCases;

public interface ITestCase
{
    string Id { get; }
    string Description { get; }
    TestCategory Category { get; }
    string? RfcReference { get; }
    bool Scored { get; }
    RfcLevel RfcLevel { get; }
    ExpectedBehavior Expected { get; }
}
