---
title: "CL-LEADING-ZEROS-OCTAL"
description: "CL-LEADING-ZEROS-OCTAL test documentation"
weight: 49
---

| | |
|---|---|
| **Test ID** | `SMUG-CL-LEADING-ZEROS-OCTAL` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6) |
| **Requirement** | SHOULD |
| **Expected** | `400` or `2xx` |

## What it sends

Content-Length with a leading-zero value that differs between decimal and octal interpretation: `Content-Length: 0200` with 200 bytes of body (`A` repeated).

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 0200\r\n
\r\n
AAAAAAAAAA... (200 bytes)
```


## What the RFC says

> "Content-Length = 1*DIGIT" — RFC 9110 §8.6

The value `0200` matches the `1*DIGIT` grammar (four digits), so it is technically valid. However, `0200` can be parsed as decimal 200 or octal 128 depending on the parser implementation. This is the critical ambiguity that leading zeros create.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (RFC-valid but dangerous in proxy chains).

## Why it matters

This is a classic smuggling vector. If a front-end proxy reads `0200` as decimal 200, it forwards all 200 bytes as the body. If the back-end reads `0200` as octal 128, it only consumes 128 bytes — the remaining 72 bytes "spill" into the connection and are interpreted as the start of the next request. An attacker can craft those 72 bytes to be a complete malicious request, achieving request smuggling through parser disagreement on a single Content-Length value.

## Sources

- [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6)
- [RFC 9112 §6.3](https://www.rfc-editor.org/rfc/rfc9112#section-6.3)
