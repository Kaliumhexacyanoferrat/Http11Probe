namespace Http11Probe.Client;

public enum ConnectionState
{
    Open,
    ClosedByServer,
    TimedOut,
    Error
}
