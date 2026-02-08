---
title: "CL-DOUBLE-ZERO"
description: "CL-DOUBLE-ZERO test documentation"
weight: 48
---

| | |
|---|---|
| **Test ID** | `SMUG-CL-DOUBLE-ZERO` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6) |
| **Requirement** | SHOULD |
| **Expected** | `400` or `2xx` |

## What it sends

Content-Length with a double-zero value: `Content-Length: 00`.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 00\r\n
\r\n
```


## What the RFC says

> "Content-Length = 1*DIGIT" — RFC 9110 §8.6

The value `00` matches the `1*DIGIT` grammar (two digits), so it is technically valid per the RFC. However, leading zeros create ambiguity when parsers interpret them differently — particularly when some treat leading-zero values as octal notation.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (RFC-valid but risky in proxy chains).

## Why it matters

While `00` happens to equal `0` in both decimal and octal, accepting leading zeros sets a precedent. If a server accepts `00`, it likely also accepts `010` (decimal 10 vs. octal 8) or `0200` (decimal 200 vs. octal 128). The safer behavior is to reject any Content-Length with leading zeros to eliminate the entire class of octal ambiguity attacks.

## Sources

- [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6)
