---
title: "CL-LEADING-ZEROS"
description: "CL-LEADING-ZEROS test documentation"
weight: 3
---

| | |
|---|---|
| **Test ID** | `SMUG-CL-LEADING-ZEROS` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6) |
| **Expected** | `400` or `2xx` |

## What it sends

Content-Length with leading zeros: `Content-Length: 005`.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 005\r\n
\r\n
hello
```


## What the RFC says

The `Content-Length` grammar is `1*DIGIT`. Since `005` matches `1*DIGIT`, it is technically valid. However, leading zeros create ambiguity — some parsers may interpret the value as octal (base-8), while others treat it as decimal.

## Why it matters

This is a **security vs. strict RFC compliance** tension. The value `005` is grammatically valid, so a server that accepts it and parses it as decimal 5 is not violating the RFC. However, if a front-end and back-end disagree on whether `005` means 5 (decimal) or 5 (octal), they agree by coincidence. For values like `010` (decimal 10 vs. octal 8), disagreement causes body boundary misalignment — a smuggling vector.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (RFC-valid but potentially risky in proxy chains).

## Sources

- [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6)
