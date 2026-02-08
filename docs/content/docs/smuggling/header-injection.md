---
title: "HEADER-INJECTION"
description: "HEADER-INJECTION test documentation"
weight: 12
---

| | |
|---|---|
| **Test ID** | `SMUG-HEADER-INJECTION` |
| **Category** | Smuggling (Unscored) |
| **Expected** | `400` or close |

## What it sends

A header value containing CRLF followed by an injected header line — attempting to inject additional headers via a field value.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
X-Test: val\r\n
Injected: yes\r\n
\r\n
```

On the wire this is two well-formed headers. The test checks that the server parses them as separate headers rather than treating the CRLF as part of the `X-Test` value.


## Why it matters

If the server doesn't validate field values for CRLF sequences, an attacker can inject arbitrary headers. This can lead to response splitting, cache poisoning, or session hijacking.

## Sources

- [RFC 9110 Section 5.5](https://www.rfc-editor.org/rfc/rfc9110#section-5.5)
