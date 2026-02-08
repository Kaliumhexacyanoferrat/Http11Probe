---
title: "BINARY-GARBAGE"
description: "BINARY-GARBAGE test documentation"
weight: 1
---

| | |
|---|---|
| **Test ID** | `MAL-BINARY-GARBAGE` |
| **Category** | Malformed Input |
| **Expected** | `400`, close, or timeout |

## What it sends

Random binary bytes that do not constitute any valid HTTP message.

```
[256 bytes of pseudorandom binary data, seeded RNG(42)]
```

Not a valid HTTP request — raw binary bytes with no recognizable structure.


## Why timeout is acceptable

The server receives bytes that cannot be parsed as an HTTP request-line. It may not even determine that a request was attempted. Waiting for more data (and eventually timing out) is valid.

## Sources

- No specific RFC section -- this is a robustness test
