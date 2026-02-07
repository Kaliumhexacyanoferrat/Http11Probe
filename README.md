# Http11Probe

HTTP/1.1 server compliance and hardening tester. Sends malformed, ambiguous, and oversized requests over raw TCP sockets and checks responses against RFC 9110/9112 requirements.

## Test Categories

- **Compliance** — RFC 9110/9112 protocol requirements (bare LF, obs-fold, missing Host, invalid versions, etc.)
- **Smuggling** — CL/TE ambiguity, duplicate Content-Length, pipeline probes, obfuscation vectors
- **Malformed Input** — Binary garbage, oversized URLs/headers, control characters, integer overflow

## Usage

```
dotnet run --project src/Http11Probe.Cli -- --host localhost --port 8080
```

### Options

| Flag | Description | Default |
|------|-------------|---------|
| `--host` | Target host | `localhost` |
| `--port` | Target port | `8080` |
| `--category` | Filter by category | all |
| `--timeout` | Connect/read timeout (seconds) | `5` |
| `--output` | Write JSON report to file | — |

## Building

```
dotnet build Http11Probe.slnx
```

## Results

See the [live comparison](https://MDA2AV.github.io/Http11Probe/probe-results/) across 12 HTTP servers.
