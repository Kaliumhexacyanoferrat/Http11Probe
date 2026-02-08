---
title: "TE-HTTP10"
description: "TE-HTTP10 test documentation"
weight: 17
---

| | |
|---|---|
| **Test ID** | `SMUG-TE-HTTP10` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

HTTP/1.0 request with `Transfer-Encoding: chunked` and `Content-Length: 5`.

```http
POST / HTTP/1.0\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
Content-Length: 5\r\n
\r\n
hello
```

Note the HTTP/1.0 version — Transfer-Encoding is not defined for HTTP/1.0.


## What the RFC says

> "A server MUST NOT send a response containing Transfer-Encoding unless the corresponding request indicates HTTP/1.1 (or later)." Transfer-Encoding is not defined in HTTP/1.0.

## Why it matters

HTTP/1.0 doesn't support chunked encoding. A server that processes TE on a 1.0 request may disagree with proxies that use CL.

## Sources

- [RFC 9112 Section 6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1)
