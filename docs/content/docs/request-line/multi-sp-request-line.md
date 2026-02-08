---
title: "MULTI-SP-REQUEST-LINE"
description: "MULTI-SP-REQUEST-LINE test documentation"
weight: 1
---

| | |
|---|---|
| **Test ID** | `RFC9112-3-MULTI-SP-REQUEST-LINE` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §3](https://www.rfc-editor.org/rfc/rfc9112#section-3) |
| **Requirement** | SHOULD reject, MAY parse leniently |
| **Expected** | `400` or `2xx` |

## What it sends

A request-line with multiple spaces between components: `GET  /  HTTP/1.1` (double spaces).

```http
GET  / HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

Note the double space between `GET` and `/`.


## What the RFC says

The request-line grammar is `method SP request-target SP HTTP-version CRLF` where `SP` is exactly one space. Multiple spaces do not match this grammar, making the request-line invalid. Recipients SHOULD respond with 400.

However, RFC 9112 §3 also states:

> "Although the request-line grammar rule requires that each of the component elements be separated by a single SP octet, recipients MAY instead parse on whitespace-delimited word boundaries."

This means a server that collapses multiple spaces and processes the request is also RFC-compliant.

**Pass:** Server rejects with `400` (strict, follows SHOULD).
**Warn:** Server accepts and responds `2xx` (RFC-valid per MAY parse leniently).

## Why it matters

Some parsers are lenient and collapse multiple spaces. If a front-end collapses spaces but a back-end does not, they may parse the method, target, or version differently — leading to routing confusion or bypass.

## Sources

- [RFC 9112 §3 — Request Line](https://www.rfc-editor.org/rfc/rfc9112#section-3)
