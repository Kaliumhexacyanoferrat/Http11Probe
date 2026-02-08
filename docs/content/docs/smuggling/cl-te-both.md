---
title: "CL-TE-BOTH"
description: "CL-TE-BOTH test documentation"
weight: 1
---

| | |
|---|---|
| **Test ID** | `RFC9112-6.1-CL-TE-BOTH` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 §6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1) |
| **Requirement** | "ought to" handle as error |
| **Expected** | `400` or `2xx` |

## What it sends

A request with both `Content-Length` and `Transfer-Encoding` headers present.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 6\r\n
Transfer-Encoding: chunked\r\n
\r\n
0\r\n
\r\n
```


## What the RFC says

> "If a message is received with both a Transfer-Encoding and a Content-Length header field, the Transfer-Encoding overrides the Content-Length. Such a message might indicate an attempt to perform request smuggling or response splitting and **ought to** be handled as an error."

The "ought to" language is between SHOULD and MAY. RFC 9112 §6.3 further clarifies:

> "If a Transfer-Encoding header field is present and the chunked transfer coding is the final encoding, the message body length is determined by reading and decoding the chunked data..."

This means a server **MAY** reject the message or process it using Transfer-Encoding alone — both are RFC-compliant.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (RFC-valid, using TE to determine body length).

## Why it matters

This is **the** classic request smuggling setup. If the front-end uses Content-Length and the back-end uses Transfer-Encoding (or vice versa), they disagree on body boundaries.

## Sources

- [RFC 9112 §6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1)
- [RFC 9110 §16.3.1](https://www.rfc-editor.org/rfc/rfc9110#section-16.3.1)
