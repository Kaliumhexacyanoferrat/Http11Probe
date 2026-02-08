---
title: "TE-TRAILING-COMMA"
description: "TE-TRAILING-COMMA test documentation"
weight: 51
---

| | |
|---|---|
| **Test ID** | `SMUG-TE-TRAILING-COMMA` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §5.6.1](https://www.rfc-editor.org/rfc/rfc9110#section-5.6.1) |
| **Requirement** | SHOULD |
| **Expected** | `400` or `2xx` |

## What it sends

Transfer-Encoding with a trailing comma after `chunked`, alongside Content-Length.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked,\r\n
Content-Length: 5\r\n
\r\n
hello
```

The `Transfer-Encoding` value is `chunked,` — a trailing comma produces an empty list element after `chunked`.


## What the RFC says

> "A recipient MUST parse and ignore a reasonable number of empty list elements." — RFC 9110 §5.6.1

The trailing comma creates an empty list element. Per §5.6.1, the server should strip the empty element and see just `chunked`. However, some parsers reject the value because the trailing comma makes it syntactically unusual, while others strip it and process normally.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (RFC-valid per §5.6.1 empty-element handling).

## Why it matters

When Content-Length is also present, parser disagreement on whether `chunked,` is valid Transfer-Encoding creates a CL/TE desync. A parser that rejects the trailing comma falls back to Content-Length framing, while a parser that strips the empty element uses chunked framing. This is the mirror of the leading-comma test (SMUG-TE-LEADING-COMMA) and exploits the same §5.6.1 ambiguity from the opposite direction.

## Sources

- [RFC 9110 §5.6.1 — Lists](https://www.rfc-editor.org/rfc/rfc9110#section-5.6.1)
- [RFC 9112 §6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1)
