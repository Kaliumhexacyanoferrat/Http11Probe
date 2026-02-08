---
title: "POST-CL-HUGE-NO-BODY"
description: "POST-CL-HUGE-NO-BODY test documentation"
weight: 26
---

| | |
|---|---|
| **Test ID** | `MAL-POST-CL-HUGE-NO-BODY` |
| **Category** | Malformed Input |
| **RFC** | [RFC 9112 Section 6.2](https://www.rfc-editor.org/rfc/rfc9112#section-6.2) |
| **Expected** | `400`/close/timeout |

## What it sends

A POST request declaring a ~1GB body via Content-Length but sending no body data at all.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 999999999\r\n
\r\n
```

No body follows the empty line. The connection remains open.

## Why it matters

Tests whether the server pre-allocates memory for the declared body size or waits for data to arrive. A server that allocates 1GB upfront from a Content-Length header is vulnerable to memory exhaustion DoS -- an attacker can send many such requests cheaply to exhaust server memory. The correct behavior is to either stream the body incrementally, reject absurdly large Content-Length values, or timeout waiting for the body data that never arrives.

## Sources

- [RFC 9112 Section 6.2 -- Content-Length](https://www.rfc-editor.org/rfc/rfc9112#section-6.2)
- [CWE-770 -- Allocation of Resources Without Limits or Throttling](https://cwe.mitre.org/data/definitions/770.html)
